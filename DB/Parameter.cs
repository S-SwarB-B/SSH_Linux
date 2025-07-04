﻿using System;
using System.Collections.Generic;

namespace SSHProject.DB;

public partial class Parameter
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ServerParameter> ServerParameters { get; set; } = new List<ServerParameter>();
}
