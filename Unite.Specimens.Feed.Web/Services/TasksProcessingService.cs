﻿using System;
using System.Linq;
using Unite.Data.Entities.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Data.Services;

namespace Unite.Specimens.Feed.Web.Services
{
    public class TasksProcessingService
    {
        private readonly DomainDbContext _dbContext;


        public TasksProcessingService(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Process(TaskType type, TaskTargetType targetType, int bucketSize, Action<Task[]> handler)
        {
            while (true)
            {
                var tasks = _dbContext.Set<Task>()
                    .Where(task => task.TypeId == type && task.TargetTypeId == targetType)
                    .OrderByDescending(task => task.Date)
                    .Take(bucketSize)
                    .ToArray();

                if (tasks != null && tasks.Any())
                {
                    handler.Invoke(tasks);

                    _dbContext.RemoveRange(tasks);
                    _dbContext.SaveChanges();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
