function Confirm(itemId, itemCost)
{
    const money = document.getElementById("money");
    if (itemCost > money.innerText) {
        ReactDOM.render(<Error message={"You lack sufficient funds"} />, document.getElementById('error'));
        return;
    }
    const button = document.getElementById(itemId);
    button.innerText = "Sure?";
    button.setAttribute("onclick", `Buy(${itemId}, ${itemCost})`);
}

function Buy(itemId, itemCost)
{
    axios.post(`/Game/Shop/`,
            {
                Id: itemId,
                Cost: itemCost
            })
        .then(res =>
        {
            if (res.status === 201)
            {
                const money = document.getElementById("money");
                money.innerText -= itemCost;
            }
            const button = document.getElementById(itemId);
            button.innerText = "Buy";
            button.setAttribute("onclick", `Confirm(${itemId}, ${itemCost})`);
        });
    ;
}