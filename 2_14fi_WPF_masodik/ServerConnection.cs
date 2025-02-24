using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//nuget packege: newtonsoft.json
using Newtonsoft.Json;
using System.Net.Http;

namespace _2_14fi_WPF_masodik
{
    class ServerConnection
    {
        HttpClient client = new HttpClient();
        public async void Registration(string username, string password) {
            string url = "http://127.1.1.1:3000/register";
            try
            {
                var jsonData = new
                {
                    registerUsername = username,
                    registerPassword = password
                };
                //JSON.stringify(jsonData)
                string stringifiedJson = JsonConvert.SerializeObject(jsonData);
                //Itt adjuk meg hova küldjük és mi lesz a headerben
                StringContent sendThis = new StringContent(stringifiedJson, Encoding.UTF8, "Application/JSON");
                //Ő a send és a ReadyState == XMLHttpRequest.DONE
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                //Megnézi, hogy a status code nem 4-el vagy 5-el kezdődik
                response.EnsureSuccessStatusCode();
                //kiolvassuk a responsból hogy mit küldött a szerver
                string responseText = await response.Content.ReadAsStringAsync();
                //Átalakítjuk a kapott string információt a megfelelő típusú objektumra, ami egyezik a küldött json objektummal
                JsonResponse responseJson = JsonConvert.DeserializeObject<JsonResponse>(responseText);
                //A szerver üzenetének kiírása
                System.Windows.MessageBox.Show(responseJson.message);

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
            

        }
        public async void Login(string username, string password)
        {
            string url = "http://127.1.1.1:3000/login";
            try
            {
                var jsonData = new
                {
                    loginUsername = username,
                    loginPassword = password
                };
                //JSON.stringify(jsonData)
                string stringifiedJson = JsonConvert.SerializeObject(jsonData);
                System.Windows.MessageBox.Show(stringifiedJson);
                //Itt adjuk meg mit küldünk és mi lesz a headerben
                StringContent sendThis = new StringContent(stringifiedJson, Encoding.UTF8, "Application/JSON");
                //Ő a send és a ReadyState == XMLHttpRequest.DONE
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                //Megnézi, hogy a status code nem 4-el vagy 5-el kezdődik
                response.EnsureSuccessStatusCode();
                //kiolvassuk a responsból hogy mit küldött a szerver
                string responseText = await response.Content.ReadAsStringAsync();
                //Átalakítjuk a kapott string információt a megfelelő típusú objektumra, ami egyezik a küldött json objektummal
                JsonResponse responseJson = JsonConvert.DeserializeObject<JsonResponse>(responseText);
                responseJson.username = username;
                Dataa.users.Add(responseJson);
                //A szerver üzenetének kiírása
                System.Windows.MessageBox.Show(responseJson.token,responseJson.message);

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }


        }
        public async void Save(JsonResponse data, int result)
        {
            string url = "http://127.1.1.1:3000/save";
            try
            {
                var jsonData = new
                {
                    nyerte = result
                };
                //JSON.stringify(jsonData)
                string stringifiedJson = JsonConvert.SerializeObject(jsonData);
                System.Windows.MessageBox.Show(stringifiedJson);
                //Itt adjuk meg mit küldünk és mi lesz a headerben
                StringContent sendThis = new StringContent(stringifiedJson, Encoding.UTF8, "Application/JSON");
                // kell az authorization header: (setRequestHeader)
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue ("Bearer", data.token);
                //Ő a send és a ReadyState == XMLHttpRequest.DONE
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                //Megnézi, hogy a status code nem 4-el vagy 5-el kezdődik
                response.EnsureSuccessStatusCode();
                //kiolvassuk a responsból hogy mit küldött a szerver
                string responseText = await response.Content.ReadAsStringAsync();
                //Átalakítjuk a kapott string információt a megfelelő típusú objektumra, ami egyezik a küldött json objektummal
                JsonResponse responseJson = JsonConvert.DeserializeObject<JsonResponse>(responseText);
                //A szerver üzenetének kiírása
                System.Windows.MessageBox.Show(responseJson.token, responseJson.message);

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }


        }
    }
}
