﻿using System.Linq;
using FluentAssertions;
using Nest17.Tests.MockData;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Integration.Core
{
	[TestFixture]
	public class UpdateIntegrationTests : IntegrationTests
	{
		[Test]
		public void TestUpdate()
		{
			var project = this.Client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			var update = this.Client.Update<ElasticsearchProject>(u => u
				.IdFrom(project)
				.Script("ctx._source.loc += 10")
				.Fields("_source", "loc")
				.RetryOnConflict(5)
				.DetectNoop()
				.Refresh()
			);
			update.IsValid.Should().BeTrue();
			project = this.Client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");

			update.Get.Should().NotBeNull();
			update.Get.Found.Should().BeTrue();
			update.Source<ElasticsearchProject>().Should().NotBeNull();
			update.Source<ElasticsearchProject>().LOC.Should().Be(loc + 10);
			var fieldLoc = update.Fields<ElasticsearchProject>().FieldValues(p => p.LOC);
			fieldLoc.Should().HaveCount(1);
			fieldLoc.First().Should().Be(loc + 10);

		}

		[Test]
		public void TestUpdate_ObjectInitializer()
		{
			var id = NestTestData.Data.Last().Id;
			var project = this.Client.Source<ElasticsearchProject>(s => s.Id(id));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this.Client.Update<ElasticsearchProject>(new UpdateRequest<ElasticsearchProject>(project.Id)
			{
				RetryOnConflict = 5,
				Refresh = true,
				Script = "ctx._source.loc += 10",
			});
			project = this.Client.Source<ElasticsearchProject>(s => s.Id(id));
			project.LOC.Should().Be(loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}

		public class ElasticsearchProjectLocUpdate
		{
			public int Id { get; set; }
			[ElasticProperty(Name = "loc", AddSortField = true)]
			public int LOC { get; set; }
		}

		[Test]
		public void DocAsUpsert()
		{
			var project = this.Client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this.Client.Update<ElasticsearchProject, ElasticsearchProjectLocUpdate>(u => u
				.Id(1)
				.Doc(new ElasticsearchProjectLocUpdate
				{
					Id = project.Id,
					LOC = project.LOC + 10
				})
				.DocAsUpsert()
				.Refresh()
			);
			project = this.Client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}
	}
}
