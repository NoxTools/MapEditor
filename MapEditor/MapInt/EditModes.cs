/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 28.11.2015
 */
using System;

namespace MapEditor.MapInt
{
	public enum EditMode
    {
        WALL_PLACE,
        WALL_BRUSH,
        WALL_CHANGE, // WallProperties
        FLOOR_PLACE,
        FLOOR_BRUSH,
        EDGE_PLACE,
        OBJECT_PLACE,
        OBJECT_SELECT,
        WAYPOINT_PLACE,
        WAYPOINT_SELECT,
        WAYPOINT_CONNECT,
        POLYGON_RESHAPE
    };
}
