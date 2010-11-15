/*
This code is derived from jgit (http://eclipse.org/jgit).
Copyright owners are documented in jgit's IP log.

This program and the accompanying materials are made available
under the terms of the Eclipse Distribution License v1.0 which
accompanies this distribution, is reproduced below, and is
available at http://www.eclipse.org/org/documents/edl-v10.php

All rights reserved.

Redistribution and use in source and binary forms, with or
without modification, are permitted provided that the following
conditions are met:

- Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer.

- Redistributions in binary form must reproduce the above
  copyright notice, this list of conditions and the following
  disclaimer in the documentation and/or other materials provided
  with the distribution.

- Neither the name of the Eclipse Foundation, Inc. nor the
  names of its contributors may be used to endorse or promote
  products derived from this software without specific prior
  written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using NGit.Revwalk;
using Sharpen;

namespace NGit.Revwalk
{
	internal class FIFORevQueueTest : RevQueueTestCase<FIFORevQueue>
	{
		protected internal override FIFORevQueue Create()
		{
			return new FIFORevQueue();
		}

		/// <exception cref="System.Exception"></exception>
		[NUnit.Framework.Test]
		public override void TestEmpty()
		{
			base.TestEmpty();
			NUnit.Framework.Assert.AreEqual(0, q.OutputType());
		}

		/// <exception cref="System.Exception"></exception>
		[NUnit.Framework.Test]
		public virtual void TestCloneEmpty()
		{
			q = new FIFORevQueue(AbstractRevQueue.EMPTY_QUEUE);
			NUnit.Framework.Assert.IsNull(q.Next());
		}

		/// <exception cref="System.Exception"></exception>
		[NUnit.Framework.Test]
		public virtual void TestAddLargeBlocks()
		{
			AList<RevCommit> lst = new AList<RevCommit>();
			for (int i = 0; i < 3 * BlockRevQueue.Block.BLOCK_SIZE; i++)
			{
				RevCommit c = Commit();
				lst.AddItem(c);
				q.Add(c);
			}
			for (int i_1 = 0; i_1 < lst.Count; i_1++)
			{
				NUnit.Framework.Assert.AreSame(lst[i_1], q.Next());
			}
		}

		/// <exception cref="System.Exception"></exception>
		[NUnit.Framework.Test]
		public virtual void TestUnpopAtFront()
		{
			RevCommit a = Commit();
			RevCommit b = Commit();
			RevCommit c = Commit();
			q.Add(a);
			q.Unpop(b);
			q.Unpop(c);
			NUnit.Framework.Assert.AreSame(c, q.Next());
			NUnit.Framework.Assert.AreSame(b, q.Next());
			NUnit.Framework.Assert.AreSame(a, q.Next());
		}
	}
}
