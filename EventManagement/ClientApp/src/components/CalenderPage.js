import React, { Component } from 'react';
import EventService, {queryEvents} from '../services/eventService'
import { Calender } from './Calender';
import { getDaysInMonth } from '../utils/calender';
import { isArray } from 'util';
import { monthInWords } from '../statics/dates';
import '../css/Style.css';


export class CalenderPage extends Component {

    constructor(props) {
        super(props);
        this.state = { eventsData: [], month:0, loading: true};
    }

    async componentDidMount() {
        let month = null;
        let year = null;
        //TODO Get Month and Year from a control, so that user can select
        //month and year
        const days = getDaysInMonth(month, year);
        //make api call
        const result = await queryEvents(1,"", days[0].getMonth()+1);
        this.setState({
            eventsData: result,
            days: days,
            month: days[0].getMonth(),
            loading: false
        })
    }

    static renderCalender(eventsData, days) {
        return (
            <div> 
                <Calender eventsData={eventsData} days={days} />
            </div>
            );
    }

    render() {
        let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : CalenderPage.renderCalender(this.state.eventsData, this.state.days)
        return (
            <div>
                <h5>{monthInWords(this.state.month)}</h5>
                {contents}      
            </div>
        );
    }
}

/*TODO
 * Create Add New Event Form
 * Create Button on each Calender day which on click displays the add new event form
 * It can be a button that is visible on hover
 * Add view that lists all events when the day is hovered over
 * Add Details view that displays event details, when the event is clicked on the display
 * 
 
 */

