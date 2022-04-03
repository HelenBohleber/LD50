using System;
using System.Collections.Generic;
using Achieving;
using Inventory;
using ItemProperty;
using RoomScene;
using UnityEngine;

namespace Interaction
{
    public class TrashCollector : MonoBehaviour, IInteractable
    {
        private IMediator _mediator;
        private readonly List<ItemType> _interactableType = new List<ItemType> { ItemType.PaperTrash };
        private readonly List<ItemType> _interactableBadType = new List<ItemType> { ItemType.AppleSlice, ItemType.Apple, ItemType.CatFood };

        private void Start()
        {
            _mediator = GameObject.FindWithTag("WorldMediator").GetComponent<WorldMediator>();
        }

        private void OnMouseOver()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(200,200,200, 255);
        }

        private void OnMouseExit()
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        private void OnMouseDown()
        {
            _mediator.ShouldMove(false);
            StartInteraction();
        }
        
        public void ShowContextMenu()
        {
            throw new System.NotImplementedException();
        }

        public void ShowText()
        {
            throw new System.NotImplementedException();
        }

        public void StartInteraction()
        {
            _mediator.InteractionManagerHasSelectedItem(this);
        }

        public void Interact(InventoryItem item)
        {
            Debug.Log("interact with " + item.ItemType);
            if (_interactableType.Contains(item.ItemType))
            {
                _mediator.RemoveAndHideInventory(item);
                _mediator.ShowAchievement(AchievementType.CleanedStevesRoom);
            }
            else if (_interactableBadType.Contains(item.ItemType))
            {
                //TODO show message: are you wasting food?
            }
            else
            {
                //TODO show error text
            }
            _mediator.ShouldMove(true);
        }
    }
}