var itemToBuy = null;

function Confirm(item) {
    const money = document.getElementById("money");
    if (item.Cost > money.innerText) {
        ReactDOM.render(<Error message={"You lack sufficient funds"}/>, document.getElementById('topNotification'));
        return;
    }

    if (itemToBuy !== null) {
        const oldButton = document.getElementById(itemToBuy.Id);
        oldButton.innerText = "Buy";
        oldButton.setAttribute("onclick", `Confirm(${JSON.stringify(itemToBuy)})`);
    }
    const button = document.getElementById(item.Id);
    button.innerText = "Sure?";
    button.setAttribute("onclick", `Buy()`);
    itemToBuy = item;
}

function Buy() {
    axios.post(`/Game/Shop/`, itemToBuy)
        .then(res =>
        {
            if (res.status === 201)
            {
                const money = document.getElementById("money");
                money.innerText -= itemToBuy.Cost;
                ReactDOM.render(<Success message={`You successfully purchased a ${itemToBuy.Name}`} />, document.getElementById('topNotification'));
            }
            const button = document.getElementById(itemToBuy.Id);
            button.innerText = "Buy";
            button.setAttribute("onclick", `Confirm(${JSON.stringify(itemToBuy)})`);
            itemToBuy = null;
        });
    ;
}