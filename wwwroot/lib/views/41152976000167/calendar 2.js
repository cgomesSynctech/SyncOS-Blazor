$(function(){

  $('#calendar').fullCalendar({
    header: {
      left: 'prev,next today',
      center: 'title',
      right: 'month,basicWeek,basicDay'
    },
    defaultDate: '2018-11-27',
    editable: false,
    eventLimit: true, // allow "more" link when too many events
    events: [  {  title: 'All Day Event', start: '2016-01-01'  }, {  title: 'All Day Event', start: '2016-01-01'  }, {  title: 'All Day Event', start: '2016-01-01'  }, {  title: 'All Day Event', start: '2016-01-01'  }, {  title: 'All Day Event', start: '2016-01-01',   color: 'purple' } ]
  });
});
