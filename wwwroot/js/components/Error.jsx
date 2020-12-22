const Error = ({ message }) => {
    const unmount = () => {
        ReactDOM.unmountComponentAtNode(document.getElementById("topNotification"));
    }

    return (
        <div id="error" className="alert alert-danger alert-dismissible fade show">
            <strong>Error:</strong> {message}
            <button type="button" className="close" onClick={unmount}>&times;</button>
        </div>
    );

}