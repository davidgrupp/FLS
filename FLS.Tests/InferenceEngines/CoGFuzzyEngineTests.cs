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
using NUnit.Framework;
using System;

namespace FLS.Tests
{
	[TestFixture]
	public class CoGFuzzyEngineTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		[TestCase(60, 40)]
		public void CoG_Trap_Defuzzify_Success(Int32 waterInputValue, Double expectedValue)
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
			var warm = water.MembershipFunctions.AddTriangle("Warm", 30, 50, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTriangle("Low", 0, 25, 50);
			var high = power.MembershipFunctions.AddTriangle("High", 25, 50, 75);


			IFuzzyEngine fuzzyEngine = new FuzzyEngine(new TrapezoidCoGDefuzzification());

			fuzzyEngine.Rules.If(water.Is(cold).Or(water.Is(warm))).Then(power.Is(high));
			fuzzyEngine.Rules.If(water.Is(hot)).Then(power.Is(low));

			//Act
			var result = fuzzyEngine.Defuzzify(new { water = waterInputValue });

			//Assert
			Assert.That(Math.Floor(result), Is.EqualTo(Math.Floor(expectedValue)));
		}

		[Test]
		[TestCase(60, 73)]
		public void CoG_Trap_Defuzzify2_Success(Int32 waterInputValue, Double expectedValue)
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 40, 70);
			var warm = water.MembershipFunctions.AddTriangle("Warm", 40, 70, 100);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 70, 100, 120, 120);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTriangle("Low", -50, 20, 50);
			var med = power.MembershipFunctions.AddTriangle("Medium", 20, 50, 100);
			var high = power.MembershipFunctions.AddTriangle("High", 50, 100, 150);


			IFuzzyEngine fuzzyEngine = new FuzzyEngine(new TrapezoidCoGDefuzzification());

			fuzzyEngine.Rules.If(water.Is(cold)).Then(power.Is(high));
			fuzzyEngine.Rules.If(water.Is(warm)).Then(power.Is(med));
			fuzzyEngine.Rules.If(water.Is(hot)).Then(power.Is(low));

			//Act
			var result = fuzzyEngine.Defuzzify(new { water = waterInputValue });

			//Assert
			Assert.That(Math.Floor(result), Is.EqualTo(Math.Floor(expectedValue)));
		}

		[Test]
		public void CoG_Trap_Defuzzify_WrongType()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 40, 70);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 70, 100, 120, 120);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddGaussian("Low", 20, 20);
			var high = power.MembershipFunctions.AddTriangle("High", 50, 100, 150);


			IFuzzyEngine fuzzyEngine = new FuzzyEngine(new TrapezoidCoGDefuzzification());

			fuzzyEngine.Rules.If(water.Is(cold)).Then(power.Is(high));
			fuzzyEngine.Rules.If(water.Is(hot)).Then(power.Is(low));

			//Act
			var result = new TestDelegate(() => fuzzyEngine.Defuzzify(new { water = 60 }));

			//Assert
			Assert.Throws(Is.InstanceOf(typeof(ApplicationException)), result, ErrorMessages.AllMembershipFunctionsMustBeTrapezoid);
		}

		[Test]
		public void CoG_Trap_Defuzzify_ZeroDemonenator_Success()
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 40, 50);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 70, 80, 100, 100);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTriangle("Low", 0, 20, 40);
			var high = power.MembershipFunctions.AddTriangle("High", 50, 100, 150);


			IFuzzyEngine fuzzyEngine = new FuzzyEngine(new TrapezoidCoGDefuzzification());

			fuzzyEngine.Rules.If(water.Is(cold)).Then(power.Is(high));
			fuzzyEngine.Rules.If(water.Is(hot)).Then(power.Is(low));

			//Act
			var result = fuzzyEngine.Defuzzify(new { water = 60 });

			//Assert
			Assert.That(result, Is.EqualTo(0), "result");
		}

		[Test]
		[TestCase(60, 71)]
		public void CoG_Defuzzify_Success(Int32 waterInputValue, Double expectedValue)
		{
			//Arrange
			LinguisticVariable water = new LinguisticVariable("Water");
			var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 40, 70);
			var warm = water.MembershipFunctions.AddTriangle("Warm", 40, 70, 100);
			var hot = water.MembershipFunctions.AddTrapezoid("Hot", 70, 100, 120, 120);

			LinguisticVariable power = new LinguisticVariable("Power");
			var low = power.MembershipFunctions.AddTriangle("Low", -50, 20, 50);
			var med = power.MembershipFunctions.AddGaussian("Medium", 60, 20);
			var high = power.MembershipFunctions.AddTriangle("High", 50, 100, 150);


			IFuzzyEngine fuzzyEngine = new FuzzyEngine(new CoGDefuzzification());

			fuzzyEngine.Rules.If(water.Is(cold)).Then(power.Is(high));
			fuzzyEngine.Rules.If(water.Is(warm)).Then(power.Is(med));
			fuzzyEngine.Rules.If(water.Is(hot)).Then(power.Is(low));

			//Act
			var result = fuzzyEngine.Defuzzify(new { water = waterInputValue });

			//Assert
			Assert.That(Math.Floor(result), Is.EqualTo(Math.Floor(expectedValue)));
		}
	}
}
