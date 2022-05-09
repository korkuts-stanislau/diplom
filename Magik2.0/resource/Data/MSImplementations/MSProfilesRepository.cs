using Microsoft.EntityFrameworkCore;
using Resource.Data.Interfaces;
using Resource.Models;

namespace Resource.Data.MSImplementations {
    public class MSProfilesRepository : IProfilesRepository
    {
        private readonly AppDbContext context;

        public MSProfilesRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(Profile profile)
        {
            await context.Profiles.AddAsync(profile);
            await context.SaveChangesAsync();
        }

        public async Task<Profile?> FirstOrDefaultAsync(string accountId)
        {
            return await context.Profiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
        }

        public async Task UpdateAsync(Profile profile)
        {
            context.Profiles.Update(profile);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profile>> GetAcceptedContactProfilesAsync(string accountId)
        {
            return await GetContactProfilesAsync(accountId, true);
        }

        public async Task<IEnumerable<Profile>> GetRequestedContactProfilesAsync(string accountId)
        {
             return await GetContactProfilesAsync(accountId, false);
        }

        private async Task<IEnumerable<Profile>> GetContactProfilesAsync(string accountId, bool accepted) {
            var profile = await FirstOrDefaultAsync(accountId);
            if(profile == null) throw new ApplicationException("Профиль не найден");
            var profilesFirst = context.Contacts
                .Where(c => c.SecondProfileId == profile.Id && c.Accepted == accepted)
                .Join(context.Profiles,
                    c => c.FirstProfileId,
                    p => p.Id,
                    (c, p) => new Profile {
                        Id = p.Id,
                        Username = p.Username,
                        Description = p.Description,
                        Icon = p.Icon
                    });
            var profilesSecond = context.Contacts
                .Where(c => c.FirstProfileId == profile.Id && c.Accepted == accepted)
                .Join(context.Profiles,
                    c => c.SecondProfileId,
                    p => p.Id,
                    (c, p) => new Profile {
                        Id = p.Id,
                        Username = p.Username,
                        Description = p.Description,
                        Icon = p.Icon
                    });
            return await profilesFirst.Concat(profilesSecond).ToListAsync();    
        }

        public async Task<IEnumerable<Profile>> SearchProfiles(string accountId, IProfilesRepository.SearchFilter filter, string query)
        {
            var profile = await FirstOrDefaultAsync(accountId);
            if(profile == null) throw new ApplicationException("Профиль не найден");
            // convert data to lower case
            query = query.ToLower();
            IQueryable<Profile>? otherProfilesQ = null;
            // filter
            switch(filter) {
                case IProfilesRepository.SearchFilter.Name:
                    otherProfilesQ = context.Profiles.Where(p => p.Username.ToLower().Contains(query) && p.AccountId != accountId);
                    break;
                case IProfilesRepository.SearchFilter.Description:
                    otherProfilesQ = context.Profiles.Where(p => p.Description.ToLower().Contains(query) && p.AccountId != accountId);
                    break;
            }
            var otherProfiles = await otherProfilesQ!
                .Select(p => new Profile {
                    Id = p.Id,
                    Username = p.Username,
                    Description = p.Description,
                    Icon = p.Icon
                }).ToListAsync();
            // filter by contacts
            var profileContactIds = context.Contacts
                .Where(c => c.FirstProfileId == profile.Id || c.SecondProfileId == profile.Id)
                .Select(c => c.FirstProfileId == profile.Id ? c.SecondProfileId : c.FirstProfileId);
            
            return otherProfiles.Where(p => !profileContactIds.Contains(p.Id));
        }
    }
}