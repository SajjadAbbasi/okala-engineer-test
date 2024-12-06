1. **Question:** How long did you spend on the coding assignment? What would you add to
   your solution if you had more time? If you didn't spend much time on the coding
   assignment then use this as an opportunity to explain what you would add.

    **Answer:**
   I spent over 20 hours on the coding assignment, ensuring that the core requirements were met with a clean and maintainable approach. If I had more time, I would focus on several key areas to enhance the solution:

   * **Code Quality and Design Patterns:**
   I would review and refactor the codebase to incorporate additional design patterns where applicable, such as Strategy and Factory(especially for different exchange rate API services), to make the code more modular, extensible.
   * **Security Enhancements**:
   I’m passionate about implementing robust security measures and would add middleware to protect against common vulnerabilities such as Cross-Site Scripting (XSS).

   * **Performance Optimization:**
   Depending on the requirements, I’d analyze the solution for potential bottlenecks and optimize external API in heavy demand situations.

   * **Documentation:**
   I’d invest more time in documenting the code and design decisions to facilitate understanding and future maintenance.

---
2. **Question:** What was the most useful feature that was added to the latest version of your
   language of choice? Please include a snippet of code that shows how you've used it.

   **Answer:**
   The latest C# features, like Primary Constructors for simplifying object initialization and Collection Expressions for cleaner collection syntax, enhance code clarity and reduce boilerplate. In .NET, Performance Improvements through advanced JIT compilation boost runtime efficiency, making applications faster and more optimized.
```` 
   public class ExchangeService(
       IExchangeRepository exchangeRepository,
       IExchangeAggregatorClient exchangeClient) : IExchangeService
   {
       public async Task<IEnumerable<ExchangeRate>> GetExchangeRateByCode(string baseCurrencyCode)
       {
           var fiatCurrencyList = exchangeRepository.GetFiatCurrencyList().Select(c => c.Code).ToArray();
           var exchangeRates = await exchangeClient.GetRateByCurrencyCode(baseCurrencyCode, fiatCurrencyList);
           return exchangeRates;
       }
   }
   ````

---
3. **Question:** How would you track down a performance issue in production? Have you ever
   had to do this?

   **Answer:** I’m a little familiar with tools like Elasticsearch, Logstash, and Kibana (ELK Stack), which can be used to monitor application performance, analyze logs, and visualize metrics. These tools are effective for identifying bottlenecks, tracking error trends, and understanding system behavior to address performance issues in production environments.
---
5. **Question:** What was the latest technical book you have read or tech conference you
   have been to? What did you
   learn?

   **Answer:** The latest technical book I’ve read is "Unit Testing: Principles, Practices and Patterns" by Vladimir Khorikov. It provided me with valuable insights into writing effective unit tests, adhering to best practices. 
In addition to reading this book, I also attended a course focused on implementing Unit Tests and Integration Tests.
The course was led by [Mohammad Reza Taghipour](https://www.linkedin.com/in/mohammadreza-taghipour/), a technical expert and lecturer. His practical approach and in-depth explanations significantly enhanced my understanding of testing strategies.
---
5. **Question:** What do you think about this technical assessment?
   
   **Answer:** The task assigned to me was challenging and provided a great opportunity to demonstrate my skills. I focused on implementing best practices and adhering to proper architectural principles throughout the solution. The deadline provided was sufficient, and at first glance, the task seemed straightforward. However, as I worked through it, I encountered scenarios that could have benefited from clearer instructions or additional context about the specific issues or concerns being evaluated. Overall, I found the assessment to be a valuable exercise that aligned well with the expectations for the role.
---
7. **Question:** Please, describe yourself using JSON.
   
   **Answer:**
```
   {
     "firstName" : "Sajjad",
     "lastName" : "Abbasi",
     "birthDate" : "1996-05-29",
     "gender":"Male",
     "maritalStatus":"Single",
     "identity":{
       "id" : "4311036132",
       "nationality":"Iranian"
     },
     "contactInfo":{
       "mobileNumber": ["+989371444124"],
       "phoneNumber":["+982150226022"]
     },
     "career":{
       "title" : ".NET Backend Developer",
       "companies":[
         {
           "name" : "Edsabco",
           "jobTitle": "Team Lead",
           "startDate":"2019-10-20",
           "endDate": null
         },
         {
           "name" : "EITD",
           "jobTitle": "Intern",
           "startDate":"2019-01-10",
           "endDate": "2018-10-21"
         }
       ],
     "educations":[
       {
         "degree": "Bachelor",
         "major": "Computer Software Engineering",
         "graduated": true,
         "university":"University of Zanjan",
         "startDate":"2015-09-06",
         "endDate" : "2019-06-05"
       }
     ],
     "skills":[".NET","TDD","C#","Software Architecture","Computer Networking"]
     },
     "passions":["Video Games","Learning","Mafia Game","Movies"]
   }

```