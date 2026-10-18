$(function(){

    $('#calendar').fullCalendar({
      header: {
        left: 'prev,next today',
        center: 'title',
        right: 'month,agendaWeek,agendaDay,listWeek'
      },
    defaultDate: '2018-12-04',
	navLinks: true, // can click day/week names to navigate views

    weekNumbers: true,
    weekNumbersWithinDays: true,
    weekNumberCalculation: 'ISO',

    editable: false,
    eventLimit: true, // allow "more" link when too many events
    events: [  { title: 'Selena Gomes-Vacina', start: '2018-12-04T07:30:00', color: '#fc878a'  }, { title: 'Selena Gomes-Vacina', start: '2018-12-04T08:30:00', end: '2018-12-04T08:30:00', color: '#fc878a'  }, { title: 'Selena Gomes-ND', start: '2018-12-05T08:00:00', end: '2018-12-05T08:00:00', color: '#9d93ff'  }, { title: 'Selena Gomes-Tosa', start: '2018-12-04', color: '#f1b4e7'  }, { title: 'Selena Gomes-Hotelzinho', start: '2018-12-13', color: '#f6b73c'  }, { title: 'Selena Gomes-Banho', start: '2018-12-12T08:30:00', end: '2018-12-14T08:30:00', color: '#66ccff'  } ]
  });
});
