using System;
using System.Collections.Generic;

namespace SSHProject;

public partial class AdditionalParametersserver
{
    public Guid? IdAdditionalParameter { get; set; }

    public Guid IdServer { get; set; }

    public virtual AdditionalParameter? IdAdditionalParameterNavigation { get; set; }

    public virtual Server IdServerNavigation { get; set; } = null!;
}
