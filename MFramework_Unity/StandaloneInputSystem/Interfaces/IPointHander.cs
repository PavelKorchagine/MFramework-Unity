using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.InputSystem
{
    public interface IPointEnterHandler
    {
        void OnPointEnter(object par);
    }

    public interface IPointExitHandler
    {
        void OnPointExit(object par);
    }

    public interface IPointDownHandler
    {
        IPointDownHandler OnPointDown(object par);
    }

    public interface IPointUpHandler
    {
        IPointUpHandler OnPointUp(object par);
    }

    public interface IPointClickHandler
    {
        void OnPointClick(object par);
    }

    public interface IPointStayHandler
    {
        void OnPointStay(object par);
    }

    public interface IPointDoubleClickHandler
    {
        void OnPointDoubleClick(object par);
    }
    public interface IPointRightMouseClickHandler
    {
        void OnPointRightMouseClick(object par);
    }

    public interface IPointDragHandler
    {
        void OnPointDrag(object par);
    }

    public interface IPointStartDragHandler
    {
        void OnPointStartDrag(object par);
    }

    public interface IPointEndDragHandler
    {
        void OnPointEndDrag(object par);
    }
}