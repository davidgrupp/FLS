﻿#region License
//   FLS - Fuzzy Logic Sharp for .NET
//   Copyright 2014 David Grupp
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
#endregion
using FLS.Constants;
using FLS.MembershipFunctions;
using NUnit.Framework;
using System;

namespace FLS.Tests.MembershipFunctions
{
	[TestFixture]
	public class BellMembershipFunctionTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		[TestCase(15, 3, 50, 50, 1)]
		[TestCase(15, 3, 50, 30, .151)]
		[TestCase(15, 3, 50, 70, .151)]
		public void Bell_Fuzzify_Success(double a, double b, double c, double inputValue, double expectedResult)
		{
			//Arrange
			var membershipFunction = new BellMembershipFunction("test", a, b, c);

			//Act
			var result = membershipFunction.Fuzzify(inputValue);

			//Assert
			Assert.That(Math.Round(result, 3), Is.EqualTo(expectedResult));
		}

		[Test]
		public void Bell_Min_Success()
		{
			//Arrange
			var membershipFunction = new BellMembershipFunction("test", 10, 50, 20);

			//Act
			var result = membershipFunction.Min();

			//Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test]
		public void Bell_Max_Success()
		{
			//Arrange
			var membershipFunction = new BellMembershipFunction("test", 10, 50, 20);

			//Act
			var result = membershipFunction.Max();

			//Assert
			Assert.That(result, Is.EqualTo(200));
		}

		[Test]
		public void Bell_InvalidInput()
		{
			//Arrange

			//Act
			var membershipFunction = new TestDelegate(() => new BellMembershipFunction("test", 0, 50, 10));

			//Assert
			Assert.Throws(Is.InstanceOf(typeof(ArgumentException)), membershipFunction, ErrorMessages.AArgumentIsInvalid);
		}
	}
}
