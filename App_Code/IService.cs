using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface ItaradisyonService
{
    [OperationContract]
    void DoWork();

    [OperationContract]
    [WebInvoke(Method = "POST",
       BodyStyle = WebMessageBodyStyle.Wrapped,
       ResponseFormat = WebMessageFormat.Json)]
    bool Login(string EmailAddress, string Password);

    [OperationContract]
    [WebInvoke(Method = "POST",
       BodyStyle = WebMessageBodyStyle.Wrapped,
       ResponseFormat = WebMessageFormat.Json)]
    bool SignUp(string FirstName, string LastName, char Gender, string Email, string Password, DateTime Birthdate, String Nationality, int Point);

    [OperationContract]
    [WebInvoke(Method = "POST",
       BodyStyle = WebMessageBodyStyle.Wrapped,
       ResponseFormat = WebMessageFormat.Json)]
    void GetUserData(int ID);
}