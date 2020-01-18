import React, { Component } from 'react';
import { formatDate } from '../utils/dateTimeLib';
import { dayOfTheWeek } from '../statics/daysOfTheWeek';

export class Day extends Component {

    constructor (props) {
        super(props);
        this.state = {eventsData: props.eventsData, loading:true};
    }

    componentDidMount() {
        this.setState({eventsData:this.props.eventsData, loading: false})
    }

    static renderDaysEvent(eventsData, day) {
        console.log("day", day, eventsData.length);
        return (
            eventsData.length < 1
                ?
            <div>
                <h6>No Events for today {day.getDate()}</h6>
            </div>
                :            
            <div>
                <ul>
                    {eventsData.map(eventData =>
                        <li key={eventData.id}>                              
                            <div className="event"> {eventData.title} {formatDate(eventData.startDate)}</div>
                        </li>
                    )}
                </ul>
            </div>
        );
    }

    render () {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Day.renderDaysEvent(this.props.eventsData, this.props.day);

        return (
            <div className={'grid-item day ' + dayOfTheWeek(this.props.day.getDay())}>
                {contents}
            </div>
        )
    }
}