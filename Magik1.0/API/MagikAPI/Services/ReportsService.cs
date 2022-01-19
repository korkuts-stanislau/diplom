using MagikAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagikAPI.Services
{
    public class ReportsService
    {
        /// <summary>
        /// УЖАСНАЯ РЕАЛИЗАЦИЯ. ПЕРЕДЕЛАТЬ ТОЛКОВО
        /// </summary>
        /// <param name="context"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<string> GetFullProjectsReport(MagikContext context, int accountId)
        {
            var currentProjects = context.ProjectAreas
                .Where(a => a.AccountId == accountId)
                .ToList()
                .Join(context.Projects.Include(p => p.ProjectParts).ToList(), a => a.Id, p => p.ProjectAreaId, (a, p) => p)
                .Join(context.ProjectParts.ToList(), p => p.Id, pp => pp.ProjectId, (p, pp) => new
                {
                    Project = p,
                    ProjectPart = pp
                })
                .GroupBy(p => p.Project)
                .Where(item => item.Any(i => i.ProjectPart.Progress != 100))
                .Select(item => item.Key);

            StringBuilder report = new StringBuilder();
            report.Append("Текущие незавершённые проекты:\n");
            foreach (var project in currentProjects)
            {
                report.Append(project.ToString() + Environment.NewLine);
            }
            return report.ToString();
        }
    }
}
