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
        public async Task<Profile?> FirstOrDefaultAsync(int profileId) {
             return await context.Profiles.FirstOrDefaultAsync(p => p.Id == profileId);
        }

        public async Task UpdateAsync(Profile profile)
        {
            context.Profiles.Update(profile);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profile>> GetAcceptedContactProfilesAsync(string accountId)
        {
            var profile = await FirstOrDefaultAsync(accountId);
            if(profile == null) throw new ApplicationException("Профиль не найден");
            var profilesFirst = context.Contacts
                .Where(c => c.SecondProfileId == profile.Id && c.Accepted)
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
                .Where(c => c.FirstProfileId == profile.Id && c.Accepted)
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

        public async Task<IEnumerable<Profile>> GetRequestedContactProfilesAsync(string accountId)
        {
            var profile = await FirstOrDefaultAsync(accountId);
            if(profile == null) throw new ApplicationException("Профиль не найден");
            var profiles = context.Contacts
                .Where(c => c.SecondProfileId == profile.Id && !c.Accepted)
                .Join(context.Profiles,
                    c => c.FirstProfileId,
                    p => p.Id,
                    (c, p) => new Profile {
                        Id = p.Id,
                        Username = p.Username,
                        Description = p.Description,
                        Icon = p.Icon
                    });
            return await profiles.ToListAsync();
        }

        public async Task<IEnumerable<Profile>> SearchProfilesAsync(string accountId, IProfilesRepository.SearchFilter filter, string query)
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

        public async Task DeleteContactAsync(string accountId, int otherProfileId) {
            var profile = await context.Profiles.FirstAsync(p => p.AccountId == accountId);
            var contact = await context.Contacts.FirstOrDefaultAsync(c => (c.FirstProfileId == profile.Id && c.SecondProfileId == otherProfileId) || 
                                                                          (c.SecondProfileId == profile.Id && c.FirstProfileId == otherProfileId));
            if(contact == null) throw new ApplicationException("У вас нет такого контакта");
            context.Contacts.Remove(contact);
            await context.SaveChangesAsync();
        }
        public async Task DeclineRequestAsync(string accountId, int otherProfileId) {
            await DeleteContactAsync(accountId, otherProfileId);
        }
        public async Task AcceptRequestAsync(string accountId, int otherProfileId) {
            var profile = await context.Profiles.FirstAsync(p => p.AccountId == accountId);
            var contact = await context.Contacts.FirstOrDefaultAsync(c => (c.FirstProfileId == profile.Id && c.SecondProfileId == otherProfileId) || 
                                                                          (c.SecondProfileId == profile.Id && c.FirstProfileId == otherProfileId));
            if(contact == null) throw new ApplicationException("У вас нет такого запроса");
            contact.Accepted = true;
            context.Contacts.Update(contact);
            await context.SaveChangesAsync();
        }
        public async Task SendRequestAsync(string accountId, int otherProfileId) {
            var profile = await context.Profiles.FirstAsync(p => p.AccountId == accountId);
            var contact = new Contact {
                FirstProfileId = profile.Id,
                SecondProfileId = otherProfileId,
                Accepted = false
            };
            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();
        }
    }
}