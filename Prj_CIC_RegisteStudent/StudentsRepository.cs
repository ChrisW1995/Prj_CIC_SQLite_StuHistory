using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Prj_CIC_RegisteStudent.Model;

namespace Prj_CIC_RegisteStudent
{
    public class StudentsRepository
    {

        private readonly StudentContext _context;

        public StudentsRepository(StudentContext context)
        {
            _context = context;
            _context.Configuration.ProxyCreationEnabled = false;
            _context.Configuration.AutoDetectChangesEnabled = false;
            _context.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<studentTestNo> GetTestNoList()
        {
            var testno_list = _context.Students.Where(x => x.year >= 105 && x.sch.Contains("中國文化大學")).GroupBy(x=>new{x.testno, x.year})
                .Select(x => new studentTestNo
                {
                    testno = x.Key.testno,
                    year = x.Key.year
                });
            return testno_list;
        }

        public IEnumerable<student> GetStudent(int _testno, int year)
        {
            return _context.Students.Where(x => x.testno == _testno && x.year == year);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
