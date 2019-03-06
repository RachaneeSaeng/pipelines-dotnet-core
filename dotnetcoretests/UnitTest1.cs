using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnetcoresample.Controllers;
using EventServiceBus;
using EventServiceBus.Events;
using dotnetcoresample.IntegrationEvents.Events;
using dotnetcoresample.IntegrationEvents.EventHandling;

namespace dotnetcoretests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void About()
        {
          // Arrange
          HomeController controller = new HomeController();

          // Act
          ViewResult result = controller.About() as ViewResult;

          // Assert
          Assert.AreEqual("Your application description page.", result.ViewData["Message"]);
        }

        [TestMethod]
        public void Contact()
        {
          // Arrange
          HomeController controller = new HomeController();

          // Act
          ViewResult result = controller.Contact() as ViewResult;

          // Assert
          Assert.AreEqual("Your contact page.", result.ViewData["Message"]);
        }

        [TestMethod]
        public void EventBus()
        {
            // Arrange
            var ebsm = new EventBusSubscriptionsManager();

            // Act
            ebsm.AddSubscription<CacheValueChangedIntegrationEvent, CacheValueChangedIntegrationEventHandler>();
        }
    }
}
