import React from 'react';
import './custom.css';
import Tweet from './Tweet.js';

const DUMMY_DATA = [
    {
        id: 'dt1',
        title: 'dummytweet1',
        text: 'first dummy tweet'
    },
    {
        id: 'dt2',
        title: 'dummytweet2',
        text: 'second dummy tweet'
    }
]

function App() {
    return (
        <section>
            <h1>Tweets of Interest</h1>
            <ul>
                {DUMMY_DATA.map((tweet) => {
                    return <li key={tweet.id}><Tweet text={tweet.text} /></li>;
                })}
            </ul>
        </section>
    );
}

export default App;
