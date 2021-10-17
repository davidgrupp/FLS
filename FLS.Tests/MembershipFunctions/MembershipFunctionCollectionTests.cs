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
using FLS.MembershipFunctions;
using NUnit.Framework;
using System.Linq;

namespace FLS.Tests.MembershipFunctions
{
	[TestFixture]
	public class MembershipFunctionCollectionTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void MembershipFunctionCollection_Types_Success()
		{
			//Arrange
			var collection = new MembershipFunctionCollection();

			//Act
			var trap = collection.AddTrapezoid("trap", 0, 0, 20, 40);
			var tri = collection.AddTriangle("tri", 20, 40, 60);
			collection.AddRectangle("rect", 10, 50);
			collection.AddGaussian("gaus", 50, 20);
			collection.AddBell("bell", 10, 3, 50);
			collection.AddSShaped("sshaped", 50, 10);
			collection.AddZShaped("zshaped", 50, 10);
			collection.AddComposite("comp", trap, tri, 20);

			//Assert
			Assert.That(collection.Any(mf => mf is TrapezoidMembershipFunction), Is.True, "Trapezoid");
			Assert.That(collection.Any(mf => mf is TriangleMembershipFunction), Is.True, "Triangle");
			Assert.That(collection.Any(mf => mf is RectangleMembershipFunction), Is.True, "Rectangle");
			Assert.That(collection.Any(mf => mf is GaussianMembershipFunction), Is.True, "Gaussian");
			Assert.That(collection.Any(mf => mf is BellMembershipFunction), Is.True, "Bell");
			Assert.That(collection.Any(mf => mf is SShapedMembershipFunction), Is.True, "SShaped");
			Assert.That(collection.Any(mf => mf is ZShapedMembershipFunction), Is.True, "ZShaped");
			Assert.That(collection.Any(mf => mf is CompositeMembershipFunction), Is.True, "Composite");
		}
	}
}
