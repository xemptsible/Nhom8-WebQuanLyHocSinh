using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Singleton
{
    public class Context
    {
        private static Context instance;
        private static readonly object lockObject = new object();
        private QLDEntities db;

        private Context()
        {
            // Khởi tạo đối tượng QLDEntities ở đây
            db = new QLDEntities();
        }

        public static Context Instance//truy cập và thể hiện insance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new Context();
                        }
                    }
                }
                return instance;
            }
        }

        public QLDEntities DbContext
        {
            get { return db; }
        }
    }
}