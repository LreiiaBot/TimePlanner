using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TimePlannerUpdated.Default
{
    static class DBTaskList
    {
        public static List<TaskList> ReadAll()
        {
            List<TaskList> lists = new List<TaskList>();
            using (var context = new TimeplannerContext())
            {
                lists = context.TaskList.Include(tl => tl.Tasks).ThenInclude(ta => ta.CustomReminders).Include(tl => tl.CustomReminders).ToList();
            }
            return lists;
        }

        public static void SaveAll(IEnumerable<TaskList> tasklists)
        {
            using (var context = new TimeplannerContext())
            {
                context.TaskList.UpdateRange(tasklists);
                //context.Entry(tasklists);
                //somehow it might have to be set "updated"
                context.SaveChanges();
            }
        }
        public static void Save(TaskList tasklist)
        {
            using (var context = new TimeplannerContext())
            {
                context.TaskList.Update(tasklist);
                //context.Entry(tasklists);
                //somehow it might have to be set "updated"
                context.SaveChanges();
            }
        }
    }
}
