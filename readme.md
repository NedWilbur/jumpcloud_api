# Jumpcloud QA Assignment

## Step 1: Exploratory testing

At this stage, I simply play with the AUT. I try all the documented features and see how they behave, then start to try things that are not documented such as passing parameters that should not work. If I had access to the source (ideally w/o decompiling), I would look for any other undocumented endpoints to test.

## Step 2: Defining test cases & Automation

Once I have a better understanding of the AUT, I do a risk assessment of the features, asking, “how big of a risk to the company if feature X fails”. Those with the highest risk are the first to be tested (typically categorized as a “Smoke” type test). I go through each feature assessing the risk to determine test case priority.

When considering what test cases are good automation candidates, you consider the risk, but also the return on investment. If a test case is low risk but requires heavy dev work, it is dropped in priority due to a longer return on investment.

For this particular project, given that I had no-predefined test cases and I was the sole QA engineer, I decided to use an “automation-first” method. I started building the base foundation for testing the AUT while also defining test cases within the code. Once I felt comfortable with the defined test cases, I developed the code required to automate them. 

This approach allowed me to develop the test cases and automation with the most efficient use of time. However, if I were on a team, I would first define the test cases in a more traditional manner ideally within a test case management tool, and then follow with developing the automation.

## Next steps: Continued automation dev & CI

- Implement automation into a CI pipeline and execute it on each new build.
- Continue building out more test cases, starting with those that are the riskiest all the way to the edge cases.

# Automation:

Automation was developed from scratch using a C# .NET solution with RestSharp. The tests can be executed via the test explorer in Visual Studio. A couple of test cases are “In progress” since I had issues with requesting a graceful shutdown via RestSharp. Given more time, I would resolve this issue to automate these test cases.
