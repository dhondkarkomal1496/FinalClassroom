using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseFistScheduler.Models;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using FinalClassroom.Models;

namespace FinalClassroom.Controllers
{
    public class UserController : Controller
    {
        ClassroomAllocationSystemEntities entities = new ClassroomAllocationSystemEntities();

        Question question = new Question();
        // GET: User
        public ActionResult UserLogin()
        {
            ViewBag.AllQuestions = new SelectList(entities.Questions.ToList(), "Question1", "Question1");
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Terrace;
            scheduler.Config.multi_day = true;//render multiday events
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            scheduler.Extensions.Add(SchedulerExtensions.Extension.Collision);
            scheduler.Extensions.Add(SchedulerExtensions.Extension.Limit);

            scheduler.Config.first_hour = 8;
            scheduler.Config.last_hour = 19;
            scheduler.XY.scroll_width = 0;
            scheduler.Config.time_step = 30; // minimum event length
            scheduler.Config.multi_day = true;
            scheduler.Config.limit_time_select = true;

            scheduler.Config.cascade_event_display = true;


            scheduler.AfterInit = new List<string>() { "attachRules();" };// The required client-side handlers have to be added after Scheduler initialization but before events load.


            scheduler.LoadData = true;
            var context = new ClassroomAllocationSystemEntities();


            scheduler.Views.Items.RemoveAt(2);//remove DayView


            var units = new UnitsView("classrooms", "roomid");

            var rooms = context.ClassRooms.ToList();
            units.Label = "Rooms";
            units.AddOptions(rooms);
            scheduler.Views.Add(units);
            scheduler.Views.Add(new WeekAgendaView());



            LightboxSelect lbsEvent = new LightboxSelect("ClassRoomId", "Classroom");
            var resEvents = from a in new ClassroomAllocationSystemEntities().ClassRooms
                                //where a.EndDate >= DateTime.Now &&
                                //       a.StartDate <= DateTime.Now
                            select new { a.ClassRoomId, a.ClassRoomName };



            foreach (var res in resEvents)
            {
                lbsEvent.AddOption(new LightboxSelectOption(res.ClassRoomId, res.ClassRoomName));
            }


            //var select = new LightboxSelect("roomid", "ClassRoom");
            //select.AddOptions(context.ClassRooms);
            
            scheduler.Lightbox.AddDefaults();
            scheduler.Lightbox.Items.Insert(1, lbsEvent);

            return View(scheduler);

        }
        public ContentResult Data()
        {
            return (new SchedulerAjaxData(
              new ClassroomAllocationSystemEntities().Events
              .Select(e => new { e.id, e.text, e.start_date, e.end_date })
              )
              );
        }
        public ContentResult Save(FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
            var entities = new ClassroomAllocationSystemEntities();
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        entities.Events.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = entities.Events.FirstOrDefault(ev => ev.id == action.SourceId);
                        entities.Events.Remove(changedEvent);
                        break;
                    default:// "update"
                        var target = entities.Events.Single(e => e.id == changedEvent.id);
                        DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
                        break;
                }
                entities.SaveChanges();
                action.TargetId = changedEvent.id;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }
    }
}