import React, { Component } from 'react';
import EventService, {getEvents} from '../services/eventService'
import { Calender } from './Calender';
import { getDaysInMonth } from '../utils/calender';
import { isArray } from 'util';


export class CalenderPage extends Component {

    constructor(props) {
        super(props);
        this.state = { eventsData: [], loading: true};
    }

    async componentDidMount() {
        const days = getDaysInMonth();
        console.log(days[0].getDate())
        //make api call,
        //TODO, get all events within the days
        const result = await getEvents(1,"");
        this.setState({eventsData:result, days:days, loading: false})
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
                {contents}      
            </div>
        );
    }
}

