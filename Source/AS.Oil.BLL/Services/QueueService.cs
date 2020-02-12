using AS.Oil.BLL.Models;
using AS.Oil.BLL.Models.DTO;
using System;
using System.Collections.Generic;

namespace AS.Oil.BLL.Services
{
    public static class QueueService
    {
        private static Queue<QueueItem> _queue = new Queue<QueueItem>();

        public static void Add(QueueItem value)
        {
            _queue.Enqueue(value);
        }

        public static QueueItem Get()
        {
            var item = (QueueItem)null;

            if(_queue.TryDequeue(out item))
                return item;

            return null;
        }

        public static int Count => _queue.Count;

        public static bool HasItem => _queue.Count > 0;
    }
}
