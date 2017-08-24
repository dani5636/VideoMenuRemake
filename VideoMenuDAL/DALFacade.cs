using System;
using System.Collections.Generic;
using System.Text;
using VideoMenuDAL.Repositories;

namespace VideoMenuDAL
{
    public class DALFacade
    {
        public IVideoRepository VideoRepository {
            //get { return new VideoRepositoryFakeDB(); }
            get { return new VideoRepositoryEFMemory(new Context.InMemoryContext()); }
        }
    }
}
