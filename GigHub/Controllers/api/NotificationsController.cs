﻿using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public NotificationsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var notifications = _unitOfWork.Notifications
                .GetUnreadNotifications(User.Identity.GetUserId())
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var notifications = _unitOfWork.UserNotifications
                .GetUnread(User.Identity.GetUserId())
                .ToList();

            notifications.ForEach(n => n.Read());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
