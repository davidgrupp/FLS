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
using FLS.Rules;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FLS.Tests.Rules
{
	[TestFixture]
	public class FuzzyRuleTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void RulePremise_Multi_Success()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
			var warm = water.MembershipFunctions.AddTrapezoid("Warm", 30, 50, 50, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTrapezoid("Low", 0, 25, 25, 50);
			var high = power.MembershipFunctions.AddTrapezoid("High", 25, 50, 50, 75);

			//Act
			var rule = new FuzzyRule().If(water.Is(cold).Or(water.Is(warm))).Then(power.Is(high));
			var result = rule.Premise;

			//Assert
			Assert.That(result, Is.Not.Empty, "result");
			Assert.That(result.Count, Is.EqualTo(2), "result count");

			Assert.That(result[0].Variable.Name, Is.EqualTo("Water"), "0 - water");
			Assert.That(result[0].MembershipFunction.Name, Is.EqualTo("Cold"), "0 - cold");

			Assert.That(result[1].Variable.Name, Is.EqualTo("Water"), "1 - water");
			Assert.That(result[1].MembershipFunction.Name, Is.EqualTo("Warm"), "1 - Warm");

			//extra
			//System.Diagnostics.Debug.WriteLine(rule.Text);
		}

		[Test]
		public void RulePremise_Single_Success()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
			var warm = water.MembershipFunctions.AddTrapezoid("Warm", 30, 50, 50, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTrapezoid("Low", 0, 25, 25, 50);
			var high = power.MembershipFunctions.AddTrapezoid("High", 25, 50, 50, 75);

			//Act
			var rule = new FuzzyRule().If(water.Is(cold)).Then(power.Is(high));
			var result = rule.Premise;

			//Assert
			Assert.That(result, Is.Not.Empty, "result");
			Assert.That(result.Count, Is.EqualTo(1), "result count");

			Assert.That(result[0].Variable.Name, Is.EqualTo("Water"), "0 - water");
			Assert.That(result[0].MembershipFunction.Name, Is.EqualTo("Cold"), "0 - cold");

			//extra
			//System.Diagnostics.Debug.WriteLine(rule.Text);
		}

		[Test]
		public void RuleConclusion_Success()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
			var warm = water.MembershipFunctions.AddTrapezoid("Warm", 30, 50, 50, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTrapezoid("Low", 0, 25, 25, 50);
			var high = power.MembershipFunctions.AddTrapezoid("High", 25, 50, 50, 75);

			//Act
			var rule = new FuzzyRule().If(water.Is(cold).Or(water.Is(warm))).Then(power.Is(high));
			var result = rule.Conclusion;

			//Assert
			Assert.That(result, Is.Not.Null, "result");
			Assert.That(result.Variable, Is.Not.Null, "result variable");
			Assert.That(result.Operator, Is.Not.Null, "result operator");
			Assert.That(result.MembershipFunction, Is.Not.Null, "result MembershipFunction");
			Assert.That(result.Variable.Name, Is.EqualTo("Power"), "result variable");
			Assert.That(result.Operator.Name, Is.EqualTo("IS"), "result operator");
			Assert.That(result.MembershipFunction.Name, Is.EqualTo("High"), "result MembershipFunction");

			//extra
			//System.Diagnostics.Debug.WriteLine(rule.Text);
		}

		[Test]
		public void RuleValid_Success()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
			var warm = water.MembershipFunctions.AddTrapezoid("Warm", 30, 50, 50, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTrapezoid("Low", 0, 25, 25, 50);
			var high = power.MembershipFunctions.AddTrapezoid("High", 25, 50, 50, 75);

			//Act
			var rule1 = new FuzzyRule().If(water.Is(cold)).Then(power.Is(high)); //valid
			var rule2 = new FuzzyRule().If(water.IsNot(cold)).Then(power.Is(high)); //valid
			var rule3 = new FuzzyRule().If(water.Is(cold).Or(water.Is(warm))).Then(power.Is(high)); //valid
			var rule4 = new FuzzyRule().If(water.Is(cold).Or(water.Is(warm)).And(water.Is(hot))).Then(power.Is(high)); //valid
			var rule5 = new FuzzyRule().If(water.Is(cold).And(water.Is(warm)).And(water.Is(hot))).Then(power.Is(high)); //valid
			var rule6 = new FuzzyRule().If(water.Is(cold).Or(water.Is(warm)).Or(water.Is(hot))).Then(power.Is(high)); //valid

			var result1 = rule1.IsValid();
			var result2 = rule2.IsValid();
			var result3 = rule3.IsValid();
			var result4 = rule4.IsValid();
			var result5 = rule5.IsValid();
			var result6 = rule6.IsValid();

			//Assert
			Assert.That(result1, Is.True, "result1");
			Assert.That(result2, Is.True, "result2");
			Assert.That(result3, Is.True, "result3");
			Assert.That(result4, Is.True, "result4");
			Assert.That(result5, Is.True, "result5");
			Assert.That(result6, Is.True, "result6");
		}

		[Test]
		public void RuleInvalid_Success()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
			var warm = water.MembershipFunctions.AddTrapezoid("Warm", 30, 50, 50, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTrapezoid("Low", 0, 25, 25, 50);
			var high = power.MembershipFunctions.AddTrapezoid("High", 25, 50, 50, 75);

			//Act
			var rule4 = new FuzzyRule().If(water.Is(cold).Or(water.Is(warm))); //not
			var rule5 = new FuzzyRule().Then(power.Is(high)); //not
			var rule6 = new FuzzyRule().If(new List<FuzzyRuleCondition>()).Then(power.Is(high)); //not

			var result1 = rule4.IsValid();
			var result2 = rule5.IsValid();
			var result3 = rule6.IsValid();

			//Assert
			Assert.That(result1, Is.False, "result1");
			Assert.That(result2, Is.False, "result2");
			Assert.That(result3, Is.False, "result3");
		}

		[Test]
		public void RuleIs_Exception()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
			var warm = water.MembershipFunctions.AddTrapezoid("Warm", 30, 50, 50, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTrapezoid("Low", 0, 25, 25, 50);
			var high = power.MembershipFunctions.AddTrapezoid("High", 25, 50, 50, 75);

			//Act
			var rule = new TestDelegate(() => new FuzzyRule().If(water.Is(null)).Then(power.Is(high)));

			//Assert
			Assert.Throws(Is.InstanceOf(typeof(ArgumentNullException)), rule);
		}

		[Test]
		public void RuleOr_Exception()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
			var warm = water.MembershipFunctions.AddTrapezoid("Warm", 30, 50, 50, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTrapezoid("Low", 0, 25, 25, 50);
			var high = power.MembershipFunctions.AddTrapezoid("High", 25, 50, 50, 75);

			//Act
			var rule = new TestDelegate(() => new FuzzyRule().If(water.Is(cold).Or(null)).Then(power.Is(high)));

			//Assert
			Assert.Throws(Is.InstanceOf(typeof(ArgumentNullException)), rule);
		}

		[Test]
		public void RuleThen_Exception()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
			var warm = water.MembershipFunctions.AddTrapezoid("Warm", 30, 50, 50, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTrapezoid("Low", 0, 25, 25, 50);
			var high = power.MembershipFunctions.AddTrapezoid("High", 25, 50, 50, 75);

			//Act
			var rule = new TestDelegate(() => new FuzzyRule().If(water.Is(cold)).Then(null));

			//Assert
			Assert.Throws(Is.InstanceOf(typeof(ArgumentNullException)), rule);
		}
	}
}
