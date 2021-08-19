using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using Xunit;

namespace RecruitmentTask
{
    public class PastEventsApiTests
    {
        private IRestResponse ExecuteGetPastEvents(string pageNumber, string language)
        {
            var restClient = new RestClient(TestConstants.WebUri);
            var endpoint = string.Format(TestConstants.BasePath, pageNumber, language);
            var request = new RestRequest(endpoint, Method.GET);
            return restClient.Execute(request);
        }

        [Fact]
        public void PastEventsPageShouldContainOnlyEventsFromPast()
        {
            int pageNumber = 0;
            string language = "en";

            List<PastEvent> responseData;
            do
            {
                var response = ExecuteGetPastEvents(pageNumber.ToString(), language);
                responseData = JsonConvert.DeserializeObject<List<PastEvent>>(response.Content);

                using (new AssertionScope())
                {
                    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                    foreach (var pastEvent in responseData)
                    {
                        DateTime startsAt = Convert.ToDateTime(pastEvent.Acf.StartsAt);
                        startsAt.Should().BeBefore(DateTime.Now);
                    }
                }
                pageNumber++;
            } while (responseData.Count != 0);
            
        }



        [Fact]
        public void ResponseWithWrongPageNumberShouldBeTheSameAsFirstPage()
        {
            string correctPageNumber = "0";
            string language = "en";
            var responseForCorrectPageNumber = ExecuteGetPastEvents(correctPageNumber, language);
            var responseForCorrectPageNumberData = JsonConvert.DeserializeObject<List<PastEvent>>(responseForCorrectPageNumber.Content);

            string incorrectPageNumber = "x";
            var responseForIncorrectPageNumber = ExecuteGetPastEvents(incorrectPageNumber, language);
            var responseForIncorrectPageNumberData = JsonConvert.DeserializeObject<List<PastEvent>>(responseForIncorrectPageNumber.Content);
            
            using (new AssertionScope())
            {
                responseForIncorrectPageNumber.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                for(int i = 0; i < responseForIncorrectPageNumberData.Count; i++)
                {
                    responseForIncorrectPageNumberData[i].Id.Should().Equals(responseForCorrectPageNumberData[i].Id);
                }
            }
        }

        [Fact]
        public void ShouldNotHaveAnyEventsWhenLanguageIsSwedish()
        {
            string pageNumber = "0";
            string language = "sv";
            var response = ExecuteGetPastEvents(pageNumber, language);
            var responseData = JsonConvert.DeserializeObject<List<PastEvent>>(response.Content);
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                responseData.Should().BeEmpty();
            }
        }

        
    }
}
