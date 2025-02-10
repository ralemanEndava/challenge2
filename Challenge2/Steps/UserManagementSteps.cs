using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Xunit;

namespace Challenge2.Steps
{
    [Binding]
    public class UserManagementSteps
    {
        private readonly HttpClient _httpClient;
        private string _name;
        private string _password;
        private string _email;
        private string _countryCode;
        private string _phoneNumber;
        private string _address;
        private string _identifier;
        private string _pin;
        private double _amount;
        private HttpResponseMessage _response;

        public UserManagementSteps()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8180") };
        }

        [Given(@"I have the following user details")]
        public void GivenIHaveTheFollowingUserDetails(Table table)
        {
            var row = table.Rows[0];
            _name = row["name"];
            _password = row["password"];
            _email = row["email"];
            _countryCode = row["countryCode"];
            _phoneNumber = row["phoneNumber"];
            _address = row["address"];
        }

        [When(@"I register the user")]
        public async Task WhenIRegisterTheUser()
        {
            var user = new
            {
                name = _name,
                password = _password,
                email = _email,
                countryCode = _countryCode,
                phoneNumber = _phoneNumber,
                address = _address
            };

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            _response = await _httpClient.PostAsync("/api/users/register", content);
        }

        [Then(@"the user should be registered successfully")]
        public void ThenTheUserShouldBeRegisteredSuccessfully()
        {
            Assert.Equal(System.Net.HttpStatusCode.OK, _response.StatusCode);
        }

        [Given(@"I have the following login details")]
        public void GivenIHaveTheFollowingLoginDetails(Table table)
        {
            var row = table.Rows[0];
            _identifier = row["identifier"];
            _password = row["password"];
        }

        [When(@"I login the user")]
        public async Task WhenILoginTheUser()
        {
            var login = new
            {
                identifier = _identifier,
                password = _password
            };

            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            _response = await _httpClient.PostAsync("/api/users/login", content);
        }

        [Then(@"the user should be logged in successfully")]
        public void ThenTheUserShouldBeLoggedInSuccessfully()
        {
            Assert.Equal(System.Net.HttpStatusCode.OK, _response.StatusCode);
        }

        [Given(@"I have the following PIN details")]
        public void GivenIHaveTheFollowingPINDetails(Table table)
        {
            var row = table.Rows[0];
            _identifier = row["accountNumber"];
            _pin = row["pin"];
            _password = row["password"];
        }

        [When(@"I create the PIN")]
        public async Task WhenICreateThePIN()
        {
            var pinDetails = new
            {
                accountNumber = _identifier,
                pin = _pin,
                password = _password
            };

            var content = new StringContent(JsonConvert.SerializeObject(pinDetails), Encoding.UTF8, "application/json");
            _response = await _httpClient.PostAsync("/api/account/pin/create", content);
        }

        [Then(@"the PIN should be created successfully")]
        public void ThenThePINShouldBeCreatedSuccessfully()
        {
            Assert.Equal(System.Net.HttpStatusCode.OK, _response.StatusCode);
        }

        [Given(@"I have the following deposit details")]
        public void GivenIHaveTheFollowingDepositDetails(Table table)
        {
            var row = table.Rows[0];
            _identifier = row["accountNumber"];
            _pin = row["pin"];
            _amount = double.Parse(row["amount"]);
        }

        [When(@"I deposit the funds")]
        public async Task WhenIDepositTheFunds()
        {
            var depositDetails = new
            {
                accountNumber = _identifier,
                pin = _pin,
                amount = _amount
            };

            var content = new StringContent(JsonConvert.SerializeObject(depositDetails), Encoding.UTF8, "application/json");
            _response = await _httpClient.PostAsync("/api/account/deposit", content);
        }

        [Then(@"the funds should be deposited successfully")]
        public void ThenTheFundsShouldBeDepositedSuccessfully()
        {
            Assert.Equal(System.Net.HttpStatusCode.OK, _response.StatusCode);
        }
    }
}

