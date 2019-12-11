import React, { Component } from 'react';
import {formatDate} from '../utils/dateTimeLib';

export class Day extends Component {

    constructor (props) {
        super(props);
        this.state = {eventsData: props.eventsData, loading:true};
    }

    componentDidMount() {
        this.setState({eventsData:this.props.eventsData, loading: false})
    }

    static renderDaysEvent (eventsData) {
        return (
            <ul>
                {eventsData.map(eventData =>
                    <li key={eventData.id}>
                         {eventData.title} 
                        <div>{formatDate(eventData.startDate)}</div>
                    </li>
                )}
            </ul>
        );
    }

    render () {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Day.renderDaysEvent(this.props.eventsData);

        return (
            <div>
                <h4>Events for today {this.props.day.getDate()}</h4>
                <div>{contents}</div>
            </div>
        )
    }
}