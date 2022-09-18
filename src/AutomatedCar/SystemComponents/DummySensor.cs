﻿namespace AutomatedCar.SystemComponents;

using Avalonia;
using Avalonia.Data;
using Models;
using Packets;
using SkiaSharp;
using System;

public class DummySensor : SystemComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DummySensor"/> class.
    /// </summary>
    /// <param name="virtualFunctionBus">We're writing all packet data here. Right now there is no thread safety mechanism, this could bite us in our behinds.</param>
    public DummySensor(VirtualFunctionBus virtualFunctionBus)
        : base(virtualFunctionBus)
    {
    }

    /// <inheritdoc/>
    public override void Process()
    {
        var circle = World.Instance.WorldObjects.Find(obj => obj is Circle);
        var car = World.Instance.WorldObjects.Find(obj => obj is AutomatedCar);

        var distance = this.CalculateDistance(circle, car);

        this.virtualFunctionBus.DummyPacket.DistanceX = (int)distance.X;
        this.virtualFunctionBus.DummyPacket.DistanceY = (int)distance.Y;
    }

    // TODO: Handle nulls correctly. Right now I don't know the expected behavior, so we simply return 0. This could cause troubles later.
    private Point CalculateDistance(WorldObject source, WorldObject destination)
    {
        if (source == null || destination == null)
        {
            return new Point(0, 0);
        }

        return new Point(
            Math.Abs(source.X - destination.X),
            Math.Abs(source.Y - destination.Y));
    }
}