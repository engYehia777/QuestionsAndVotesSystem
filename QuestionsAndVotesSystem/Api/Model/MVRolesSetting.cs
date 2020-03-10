using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionsAndVotesSystem.Api.Model
{
    public class MVRolesSetting
    {
        public int ID { get; set; }
        [Required]
        public AspNetRole Role { get; set; }
        [Required]
        public List<Roleprimission> Pages { get; set; }
        private DBEntities db = new DBEntities();


        public MVRolesSetting CreateNew(MVRolesSetting mv)
        {
            if (mv.Role == null)
                mv.Role = new AspNetRole();
            mv.Role.Id = Guid.NewGuid().ToString();
            List<Roleprimission> PagesList = new List<Roleprimission>();
            foreach (var p in db.Pages.Where(s => s.IsActive == true && s.URL != "#").ToList())
            {
                Roleprimission RolePage = new Roleprimission();
                RolePage.CanADD = false;
                RolePage.CanDelete = false;
                RolePage.CanSearch = false;
                RolePage.CanShow = false;
                RolePage.PageID = p.ID;
                RolePage.Page = p;
                RolePage.RoleId = mv.Role.Id;
                PagesList.Add(RolePage);
            }
            mv.Pages = PagesList;
            return mv;
        }
        public MVRolesSetting Update(string id)
        {

            MVRolesSetting mv = new MVRolesSetting();
            mv.Role = db.AspNetRoles.Find(id);
            List<Roleprimission> PagesList = new List<Roleprimission>();
            foreach (var p in db.Pages.Where(s => s.IsActive == true && s.URL != "#").ToList())
            {
                var EnterSelect = db.Roleprimissions.FirstOrDefault(s => s.PageID == p.ID && s.RoleId == mv.Role.Id);
                Roleprimission RolePage = new Roleprimission();
                RolePage.CanADD = EnterSelect != null ? EnterSelect.CanADD : false;
                RolePage.CanDelete = EnterSelect != null ? EnterSelect.CanDelete : false;
                RolePage.CanSearch = EnterSelect != null ? EnterSelect.CanSearch : false;
                RolePage.CanShow = EnterSelect != null ? EnterSelect.CanShow : false;
                RolePage.CanUpdate = EnterSelect != null ? EnterSelect.CanUpdate : false;
                RolePage.PageID = p.ID;
                RolePage.Page = p;
                RolePage.RoleId = mv.Role.Id;
                PagesList.Add(RolePage);
            }
            mv.Pages = PagesList;
            return mv;
        }
    }
}