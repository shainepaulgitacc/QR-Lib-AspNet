using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.AdminArea.BestUser
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        public IndexModel(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public List<BestUserModel> BestUsers { get; set; }
        public User TopOne { get; set; }
        public async Task OnGetAsync()
        {
            var rankingHolder = new List<BestUserModel>();
            var bestUsers = await _userRepo.BestUserRanking();
            var bestUserRank = bestUsers.ToList();
            var topTen = bestUserRank.Count()<=10?bestUserRank.Count():10;
            for(int x = 0; x < topTen; x++)
            {
                rankingHolder.Add(bestUserRank[x]);
            }
            BestUsers = rankingHolder;
            TopOne = await _userRepo.GetOneRecord(rankingHolder.First().UserId);
        }
    }
}
