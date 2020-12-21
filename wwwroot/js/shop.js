function Confirm(itemId, itemCost) {
    const button = document.getElementById(itemId);
    button.innerText = "Sure?";
    button.setAttribute("onclick", `Buy(${itemId}, ${itemCost})`);
}

function Buy(itemId, itemCost) {
    axios.post(`/Game/Shop/`,
            {
                Id: itemId,
                Cost: itemCost
            })
        .then(res => {
            const money = document.getElementById("money");
            money.innerText -= itemCost;
            const button = document.getElementById(itemId);
            button.innerText = "Buy";
            button.setAttribute("onclick", `Confirm(${itemId}, ${itemCost})`);
        });
    ;
}