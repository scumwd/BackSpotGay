using System.Collections;

namespace BackSpotGay.DAL.Models
{
    public class Post : IEnumerable
    {
        public string TextPost { get; set; }
        public string TextPath { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}