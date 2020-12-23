var itemIdToBuy = null;
var itemCostToBuy = null;

function Confirm(itemId, itemCost) {
    console.log("Confirm");
    const money = document.getElementById("money");
    if (itemCost > money.innerText) {
        ReactDOM.render(<Error message={"You lack sufficient funds"}/>, document.getElementById('topNotification'));
        return;
    }

    if (itemIdToBuy !== null) {
        const oldButton = document.getElementById(itemIdToBuy);
        oldButton.innerText = "Buy";
        oldButton.setAttribute("onclick", `Confirm(${itemIdToBuy}, ${itemCostToBuy})`);
    }
    const button = document.getElementById(itemId);
    button.innerText = "Sure?";
    button.setAttribute("onclick", `Buy()`);
    itemIdToBuy = itemId;
    itemCostToBuy = itemCost;
}

function Buy() {
    console.log("Buy");
    axios.post(`/Game/Shop/`,
            {
                Id: itemIdToBuy,
                Cost: itemCostToBuy
            })
        .then(res =>
        {
            if (res.status === 201)
            {
                const money = document.getElementById("money");
                money.innerText -= itemCostToBuy;
            }
            const button = document.getElementById(itemIdToBuy);
            button.innerText = "Buy";
            button.setAttribute("onclick", `Confirm(${itemIdToBuy}, ${itemCostToBuy})`);
            itemIdToBuy = null;
            itemCostToBuy = null;
        });
    ;
}