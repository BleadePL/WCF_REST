using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_REST
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MyRestService : IRestService
    {
        private static Dictionary<int, Manga> Mangas =
            new Dictionary<int, Manga>()
            {
                { 1, new Manga {Id = 1, MangaTitle = "Assasination Classroom", MangaAuthor = "Noriko Ozaki", MangaRate = 5 } },
                { 2, new Manga {Id = 2, MangaTitle = "Kakegurui", MangaAuthor = "Homura Kawamoto", MangaRate = 5}},
                { 3, new Manga {Id = 3, MangaTitle = "Tokyo Ghoul", MangaAuthor = "Sui Ishida", MangaRate = 4}}
            };

        public string addXml(Manga manga)
        {
            if (manga == null)
            {
                throw new WebFaultException<string>("400: Incorrect Data",
                    System.Net.HttpStatusCode.BadRequest);
            }

            int count = Mangas.Values.Where(m => m.MangaTitle.Equals(m.MangaTitle) && 
                                                      m.MangaAuthor.Equals(manga.MangaAuthor) && 
                                                      m.MangaRate == manga.MangaRate).Count();

            if (count > 0)
            {
                throw new WebFaultException<string>("409: The input Data Exists in the Database",
                    System.Net.HttpStatusCode.Conflict);
            }
            else
            {
                int id = Mangas.Keys.ToList().LastOrDefault() + 1;
                manga.Id = id;

                Mangas.Add(id, manga);
            }
            return $"Added element Successfully on {manga.Id} id";
        }

        public string deleteXml(string id)
        {
            int Id = int.Parse(id);

            if (Mangas.ContainsKey(Id))
            {
                Mangas.Remove(Id);
                return $"Successfully removed element at {Id} id";
            }
            else
            {
                throw new WebFaultException<string>("404: Element Not Found",
                                    System.Net.HttpStatusCode.NotFound);
            }
        }

        public string editXml(string id, Manga manga)
        {
            int Id = int.Parse(id);
            if (Mangas.ContainsKey(Id) && manga != null)
            {
                var mangaToEdit = Mangas[Id];

                mangaToEdit.MangaTitle = manga.MangaTitle;
                mangaToEdit.MangaAuthor = manga.MangaAuthor;
                mangaToEdit.MangaRate = manga.MangaRate;

                return $"Successfully Edited element on {Id} id";
            }
            else
            {
                throw new WebFaultException<string>("404: Element Not Found",
                                                    System.Net.HttpStatusCode.NotFound);
            }
        }

        public List<Manga> GetAllMangas()
        {
           return Mangas.Values.ToList();
        }

        public Manga GetByIdXml(string id)
        {
            int Id = int.Parse(id);
            if (Mangas.ContainsKey(Id))
            {
                return Mangas[Id];
            }
            else
            {
                throw new WebFaultException<string>("404: Element Not Found",
                                                    System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}
