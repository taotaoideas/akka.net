﻿using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.DI.Core
{
    /// <summary>
    /// Dependency Injection Backed IndirectActorProducer
    /// </summary>
    public class DIActorProducer : IndirectActorProducer
    {
        private IDependencyResolver dependencyResolver;
        private string actorName;
        readonly Func<ActorBase> actorFactory;

        public DIActorProducer(IDependencyResolver dependencyResolver,
                               string actorName)
        {
            if (dependencyResolver == null) throw new ArgumentNullException("dependencyResolver");
            if (actorName == null) throw new ArgumentNullException("actorName");

            this.dependencyResolver = dependencyResolver;
            this.actorName = actorName;
            this.actorFactory = dependencyResolver.CreateActorFactory(actorName);
        }
        /// <summary>
        /// The System.Type of the Actor specified in the constructor parameter actorName
        /// </summary>
        public Type ActorType
        {
            get { return this.dependencyResolver.GetType(this.actorName); }
        }
        /// <summary>
        /// Creates an instance of the Actor based on the Type specified in the constructor parameter actorName
        /// </summary>
        /// <returns></returns>
        public ActorBase Produce()
        {
            return actorFactory();
        }

    }
}
