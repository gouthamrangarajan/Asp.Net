using htmx101.Models.JsonPlaceholder;

namespace htmx101.Models{
    public class FilterViewModel{
        public string SearchTxt {get;set;}="";

        public IEnumerable<UserViewModel> Users {get;set;}=Array.Empty<UserViewModel>();
    }
}