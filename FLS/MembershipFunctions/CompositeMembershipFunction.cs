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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FLS.MembershipFunctions
{
	public class CompositeMembershipFunction : FuzzyRuleToken, IMembershipFunction
	{
		public CompositeMembershipFunction(String name, IMembershipFunction leftFunction, IMembershipFunction rightFunction, double midPoint)
			: base(name, FuzzyRuleTokenType.Function)
		{
			_leftFunction = leftFunction;
			_rightFunction = rightFunction;
			_midPoint = midPoint;
		}

		private IMembershipFunction _leftFunction;
		private IMembershipFunction _rightFunction;
		private double _midPoint;

		#region Public Methods

		public virtual Double Fuzzify(Double inputValue)
		{
			if (inputValue <= _midPoint)
			{
				return _leftFunction.Fuzzify(inputValue);
			}
			else
			{
				return _leftFunction.Fuzzify(inputValue);
			}
		}

		public virtual Double Min()
		{
			return 0;
		}

		public virtual Double Max()
		{
			return 200;
		}

		#endregion

		#region public Properties

		public Double Modification { get; set; }

		#endregion

	}
}
