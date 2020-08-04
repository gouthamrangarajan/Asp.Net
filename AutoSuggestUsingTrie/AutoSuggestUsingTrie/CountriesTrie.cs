using System.Collections.Generic;
using System.Globalization;

namespace AutoSuggestUsingTrie
{
    public class CountriesTrie
    {
        private Node _root = new Node();
        private static string[] allData=new string[] { "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Anguilla", "Antigua & Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia & Herzegovina", "Botswana", "Brazil", "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central Arfrican Republic", "Chad", "Chile", "China", "Colombia", "Congo", "Cook Islands", "Costa Rica", "Cote D Ivoire", "Croatia", "Cuba", "Curacao", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France", "French Polynesia", "French West Indies", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea Bissau", "Guyana", "Haiti", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kosovo", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius", "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauro", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "North Korea", "Norway", "Oman", "Pakistan", "Palau", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russia", "Rwanda", "Saint Pierre & Miquelon", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Korea", "South Sudan", "Spain", "Sri Lanka", "St Kitts & Nevis", "St Lucia", "St Vincent", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor L'Este", "Togo", "Tonga", "Trinidad & Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks & Caicos", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States of America", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Virgin Islands (US)", "Yemen", "Zambia", "Zimbabwe" };

        public CountriesTrie()
        {
            foreach(var dt in allData)
            {
                _root.AddString(dt.ToLower());
            }
        }

        public List<string> Search(string value)
        {
            var retList = new List<string>();
            _root.FindAll(value,retList);
            return retList;
        }

        private class Node
        {
            private Dictionary<char, Node> _children = new Dictionary<char, Node>();            
            private bool _isCompleted;
            private static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            public Node GetChild(char c)
            {
                if (_children.ContainsKey(c))
                    return _children[c];
                else
                    return null;
            }

            public void AddString(string s)
            {
                if (string.IsNullOrWhiteSpace(s))
                    return;

                addStringRecursive(s, 0);
            }
            private void addStringRecursive(string s, int index)
            {
                if (index >= s.Length)
                {
                    _isCompleted = true;
                    return;
                }
                var node = GetChild(s[index]);
                if (node == null)
                {
                    node = new Node();
                    _children[s[index]] = node;
                }
                node.addStringRecursive(s, index + 1);
            }

            public void FindAll(string value,List<string> collectedWords)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                findAllRecurison(value, 0, collectedWords);
            }

            private void findAllRecurison(string value,int index,List<string> collectedWords)
            {

                if (index>=value.Length)
                {
                    collectCompletedWordsRecursion(value, collectedWords);
                    return;
                }
                var node = GetChild(value.ToLower()[index]);
                if (node == null)
                    return;
                node.findAllRecurison(value, index + 1, collectedWords);
            }

            private void collectCompletedWordsRecursion(string prefix, List<string> collectedCountries)
            {
                if (_isCompleted)                                    
                    collectedCountries.Add(textInfo.ToTitleCase(prefix));                

                foreach(var child in _children)                
                    child.Value.collectCompletedWordsRecursion(prefix + child.Key, collectedCountries);                
            }

         
        }
    }
}
