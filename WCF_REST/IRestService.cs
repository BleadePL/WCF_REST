using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_REST
{

    [ServiceContract]
    public interface IRestService
    {
        //XML
        [OperationContract]
        [WebGet(UriTemplate = "/Manga")]
        List<Manga> GetAllMangas();

        [OperationContract]
        [WebGet(UriTemplate = "/Manga/{id}",
                ResponseFormat = WebMessageFormat.Xml)]
        Manga GetByIdXml(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Add",
                   Method = "POST",
                   ResponseFormat = WebMessageFormat.Xml)]
        string addXml(Manga manga);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Manga{id}", Method = "DELETE", ResponseFormat = WebMessageFormat.Xml)]
        string deleteXml(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/MangaEdit{id}", Method = "PUT", ResponseFormat = WebMessageFormat.Xml)]
        string editXml(string id, Manga manga);


        //JSON
        [OperationContract]
        [WebGet(UriTemplate = "/json/Manga", ResponseFormat = WebMessageFormat.Json)]
        List<Manga> GetAllMangasJson();

        [OperationContract]
        [WebGet(UriTemplate = "/json/Manga/{id}",
                ResponseFormat = WebMessageFormat.Json)]
        Manga GetByIdJson(string id);


        [OperationContract]
        [WebInvoke(UriTemplate = "/json/Add",
                   Method = "POST",
                   ResponseFormat = WebMessageFormat.Json)]
        string addJson(Manga manga);


        [OperationContract]
        [WebInvoke(UriTemplate = "/json/Manga{id}", Method = "DELETE", ResponseFormat = WebMessageFormat.Json)]
        string deleteJson(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/json/MangaEdit{id}", Method = "PUT", ResponseFormat = WebMessageFormat.Json)]
        string editJson(string id, Manga manga);

    }



    [DataContract]
    public class Manga
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember(Order = 0)]
        public string MangaTitle { get; set; }

        [DataMember(Order = 1)]
        public string MangaAuthor { get; set; }

        [DataMember(Order = 2)]
        public int MangaRate { get; set; }
    }
}
