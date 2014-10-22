using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.sjsu.mobiletaas.resourcemanager
{
    public static class AmazonHelper
    {
        public static void TerminateInstance(String instanceId)
        {
            Amazon.Runtime.BasicAWSCredentials credential = new Amazon.Runtime.BasicAWSCredentials("", "");
            Amazon.EC2.AmazonEC2Client ec2Client = new Amazon.EC2.AmazonEC2Client(credential, Amazon.RegionEndpoint.USWest2);
            var termRequest = new TerminateInstancesRequest();
            termRequest.InstanceIds = new List<string> { instanceId };
            var response = ec2Client.TerminateInstances(termRequest);
        }

        public static ServerInformation StartInstance()
        {
            Amazon.Runtime.BasicAWSCredentials credential = new Amazon.Runtime.BasicAWSCredentials("", "");
            Amazon.EC2.AmazonEC2Client ec2Client = new Amazon.EC2.AmazonEC2Client(credential, Amazon.RegionEndpoint.USWest2);
            var runInstancesRequest = new RunInstancesRequest()
            {
                ImageId = "ami-896f19b9",
                InstanceType = "g2.2xlarge",
                MinCount = 1,
                MaxCount = 1,
                KeyName = "",
                SubnetId = "subnet-3173315a",

            };
            runInstancesRequest.SecurityGroupIds.Add("sg-b236ffd7");
            RunInstancesResponse response = ec2Client.RunInstances(runInstancesRequest);
            String instanceId = response.Reservation.Instances[0].InstanceId;
            String ipAddress = response.Reservation.Instances[0].PublicIpAddress;
            while (ipAddress == null)
            {
                DescribeInstancesRequest request = new DescribeInstancesRequest();
                request.InstanceIds = new List<string> { instanceId };
                var dResponse = ec2Client.DescribeInstances(request);
                ipAddress = dResponse.Reservations[0].Instances[0].PublicIpAddress;
            }
            RestHelper helper = new RestHelper(ipAddress);
            ServerInformation information = new ServerInformation(instanceId, ipAddress);
            while (true)
            {
                try
                {
                    System.Threading.Thread.Sleep(10000);
                }
                catch (Exception ex)
                {

                }

                try
                {
                    ServerStatus s = helper.GetStatus();
                    if (s == null)
                    {
                        continue;
                    }
                    information.updateStatus(s);
                    break;
                }
                catch (Exception ex)
                {

                }

            }
            return information;
        }
    }
}
