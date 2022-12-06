using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Xunit;

namespace Microsoft.EntityFrameworkCore.SqlServer;

public class CSharpEntityTypeGeneratorTest : ModelCodeGeneratorTestBase
{
    [ConditionalFact]
    public void Class_with_DateOnly_key_is_generated()
        => Test(
            modelBuilder =>
            {
                modelBuilder.Entity(
                   "EventPlan",
                   b =>
                   {
                       b.Property<DateOnly>("StartDate");
                       b.HasKey("StartDate");
                   });
            },
            new ModelCodeGenerationOptions { UseDataAnnotations = true },
            code =>
            {
                AssertFileContents(
                    @"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestNamespace
{
    public partial class EventPlan
    {
        [Key]
        public DateOnly StartDate { get; set; }
    }
}
",
                    code.AdditionalFiles.Single(f => f.Path == "EventPlan.cs"));
            });

    [ConditionalFact]
    public void Class_with_TimeOnly_key_is_generated()
        => Test(
            modelBuilder =>
            {
                modelBuilder.Entity(
                   "EventPlan",
                   b =>
                   {
                       b.Property<TimeOnly>("StartTime");
                       b.HasKey("StartTime");
                   });
            },
            new ModelCodeGenerationOptions { UseDataAnnotations = true },
            code =>
            {
                AssertFileContents(
                    @"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestNamespace
{
    public partial class EventPlan
    {
        [Key]
        public TimeOnly StartTime { get; set; }
    }
}
",
                    code.AdditionalFiles.Single(f => f.Path == "EventPlan.cs"));
            });

    [ConditionalFact]
    public void Class_with_DateOnly_property_is_generated()
        => Test(
            modelBuilder =>
            {
                modelBuilder.Entity(
                    "EventPlan",
                    b =>
                    {
                        b.Property<int>("Id");
                        b.HasKey("Id");
                        b.Property<string>("Name");
                        b.Property<DateOnly>("DateOnly");
                    });
            },
            new ModelCodeGenerationOptions { UseDataAnnotations = true },
            code =>
            {
                AssertFileContents(
                    @"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestNamespace
{
    public partial class EventPlan
    {
        [Key]
        public int Id { get; set; }
        public DateOnly DateOnly { get; set; }
        public string Name { get; set; }
    }
}
",
                    code.AdditionalFiles.Single(f => f.Path == "EventPlan.cs"));
            });

    [ConditionalFact]
    public void Class_with_TimeOnly_property_is_generated()
        => Test(
            modelBuilder =>
            {
                modelBuilder.Entity(
                    "EventPlan",
                    b =>
                    {
                        b.Property<int>("Id");
                        b.HasKey("Id");
                        b.Property<string>("Name");
                        b.Property<TimeOnly>("TimeOnly");
                    });
            },
            new ModelCodeGenerationOptions { UseDataAnnotations = true },
            code =>
            {
                AssertFileContents(
                    @"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestNamespace
{
    public partial class EventPlan
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeOnly TimeOnly { get; set; }
    }
}
",
                    code.AdditionalFiles.Single(f => f.Path == "EventPlan.cs"));
            });

    [ConditionalFact]
    public void Class_with_multiple_DateOnly_TimeOnly_properties_are_generated()
        => Test(
            modelBuilder =>
            {
                modelBuilder.Entity(
                    "EventPlan",
                    b =>
                    {
                        b.Property<DateOnly>("Id");
                        b.HasKey("Id");
                        b.Property<string>("Name");
                        b.Property<DateOnly>("DateOnly");
                        b.Property<TimeOnly>("TimeOnly");
                    });
            },
            new ModelCodeGenerationOptions { UseDataAnnotations = true },
            code =>
            {
                AssertFileContents(
                    @"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestNamespace
{
    public partial class EventPlan
    {
        [Key]
        public DateOnly Id { get; set; }
        public DateOnly DateOnly { get; set; }
        public string Name { get; set; }
        public TimeOnly TimeOnly { get; set; }
    }
}
",
                    code.AdditionalFiles.Single(f => f.Path == "EventPlan.cs"));
            });
}
