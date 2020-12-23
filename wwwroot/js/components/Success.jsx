const Success = ({ message }) => {
    const unmount = () => {
        ReactDOM.unmountComponentAtNode(document.getElementById("topNotification"));
    }

    return (
        <div id="notification" className="alert alert-secondary alert-dismissible fade show">
            {message}
            <button type="button" className="close" onClick={unmount}>&times;</button>
        </div>
    );

}