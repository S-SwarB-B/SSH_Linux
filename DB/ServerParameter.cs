using System;
using System.Collections.Generic;

namespace SSHProject.DB;

public partial class ServerParameter
{
    public Guid Id { get; set; }

    public Guid ServerId { get; set; }

    public Guid? ParameterId { get; set; }

    public virtual Parameter? Parameter { get; set; }

    public virtual Server Server { get; set; } = null!;
}
