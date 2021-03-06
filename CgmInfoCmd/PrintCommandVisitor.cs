using System.Linq;
using CgmInfo.Commands;
using CgmInfo.Commands.ApplicationStructureDescriptor;
using CgmInfo.Commands.Attributes;
using CgmInfo.Commands.Delimiter;
using CgmInfo.Commands.Enums;
using CgmInfo.Commands.Escape;
using CgmInfo.Commands.External;
using CgmInfo.Commands.GraphicalPrimitives;
using CgmInfo.Commands.MetafileDescriptor;
using CgmInfo.Commands.PictureDescriptor;
using CgmInfo.Commands.Segment;
using CgmInfo.Traversal;

namespace CgmInfoCmd
{
    public class PrintCommandVisitor : ICommandVisitor<PrintContext>
    {
        public void AcceptDelimiterBeginMetafile(BeginMetafile beginMetafile, PrintContext parameter)
        {
            parameter.WriteLine("{0} - {1}", parameter.FileName, beginMetafile.Name);
            parameter.BeginLevel();
        }
        public void AcceptDelimiterEndMetafile(EndMetafile endMetafile, PrintContext parameter)
        {
            parameter.EndLevel();
        }
        public void AcceptDelimiterBeginPicture(BeginPicture beginPicture, PrintContext parameter)
        {
            parameter.WriteLine("Begin Picture: '{0}'", beginPicture.Name);
            parameter.BeginLevel();
        }
        public void AcceptDelimiterBeginPictureBody(BeginPictureBody beginPictureBody, PrintContext parameter)
        {
            parameter.WriteLine("Begin Picture Body");
        }
        public void AcceptDelimiterEndPicture(EndPicture endPicture, PrintContext parameter)
        {
            parameter.EndLevel();
        }
        public void AcceptDelimiterBeginSegment(BeginSegment beginSegment, PrintContext parameter)
        {
            parameter.WriteLine("Begin Segment: '{0}'", beginSegment.Identifier);
            parameter.BeginLevel();
        }
        public void AcceptDelimiterEndSegment(EndSegment endSegment, PrintContext parameter)
        {
            parameter.EndLevel();
        }
        public void AcceptDelimiterBeginFigure(BeginFigure beginFigure, PrintContext parameter)
        {
            parameter.WriteLine("Begin Figure");
            parameter.BeginLevel();
        }
        public void AcceptDelimiterEndFigure(EndFigure endFigure, PrintContext parameter)
        {
            parameter.EndLevel();
        }
        public void AcceptDelimiterBeginProtectionRegion(BeginProtectionRegion beginProtectionRegion, PrintContext parameter)
        {
            parameter.WriteLine("Begin Protection Region: {0}", beginProtectionRegion.RegionIndex);
            parameter.BeginLevel();
        }
        public void AcceptDelimiterEndProtectionRegion(EndProtectionRegion endProtectionRegion, PrintContext parameter)
        {
            parameter.EndLevel();
        }
        public void AcceptDelimiterBeginCompoundLine(BeginCompoundLine beginCompoundLine, PrintContext parameter)
        {
            parameter.WriteLine("Begin Compound Line");
            parameter.BeginLevel();
        }
        public void AcceptDelimiterEndCompoundLine(EndCompoundLine endCompoundLine, PrintContext parameter)
        {
            parameter.EndLevel();
        }
        public void AcceptDelimiterBeginCompoundTextPath(BeginCompoundTextPath beginCompoundTextPath, PrintContext parameter)
        {
            parameter.WriteLine("Begin Compound Text Path");
            parameter.BeginLevel();
        }
        public void AcceptDelimiterEndCompoundTextPath(EndCompoundTextPath endCompoundTextPath, PrintContext parameter)
        {
            parameter.EndLevel();
        }
        public void AcceptDelimiterBeginTileArray(BeginTileArray beginTileArray, PrintContext parameter)
        {
            parameter.WriteLine("Begin Tile Array: {0} by {1} tiles at {2} ({3} by {4} cells per tile)",
                beginTileArray.PathDirectionTileCount, beginTileArray.LineDirectionTileCount,
                beginTileArray.Position,
                beginTileArray.PathDirectionCellCount, beginTileArray.LineDirectionCellCount);
            parameter.BeginLevel();
        }
        public void AcceptDelimiterEndTileArray(EndTileArray endTileArray, PrintContext parameter)
        {
            parameter.EndLevel();
        }
        public void AcceptDelimiterBeginApplicationStructure(BeginApplicationStructure beginApplicationStructure, PrintContext parameter)
        {
            parameter.WriteLine("Begin Application Structure: {0} '{1}'", beginApplicationStructure.Type, beginApplicationStructure.Identifier);
            parameter.BeginLevel();
        }
        public void AcceptDelimiterBeginApplicationStructureBody(BeginApplicationStructureBody beginApplicationStructureBody, PrintContext parameter)
        {
            parameter.WriteLine("Begin Application Structure Body");
        }
        public void AcceptDelimiterEndApplicationStructure(EndApplicationStructure endApplicationStructure, PrintContext parameter)
        {
            parameter.EndLevel();
        }
        public void AcceptMetafileDescriptorMetafileVersion(MetafileVersion metafileVersion, PrintContext parameter)
        {
            parameter.WriteLine("Metafile Version: {0}", metafileVersion.Version);
        }
        public void AcceptMetafileDescriptorMetafileDescription(MetafileDescription metafileDescription, PrintContext parameter)
        {
            parameter.WriteLine("Metafile Description: {0}", metafileDescription.Description);
        }
        public void AcceptMetafileDescriptorVdcType(VdcType vdcType, PrintContext parameter)
        {
            parameter.WriteLine("VDC Type: {0}", vdcType.Specification);
        }
        public void AcceptMetafileDescriptorIntegerPrecision(IntegerPrecision integerPrecision, PrintContext parameter)
        {
            parameter.WriteLine("Integer Precision: {0} bit", integerPrecision.Precision);
        }
        public void AcceptMetafileDescriptorRealPrecision(RealPrecision realPrecision, PrintContext parameter)
        {
            if (realPrecision.Specification == RealPrecisionSpecification.Unsupported)
                parameter.WriteLine("Real Precision: Unsupported ({0}, {1} bit Exponent width, {2} bit Fraction width)",
                    realPrecision.RepresentationForm, realPrecision.ExponentWidth, realPrecision.FractionWidth);
            else
                parameter.WriteLine("Real Precision: {0}", realPrecision.Specification);
        }
        public void AcceptMetafileDescriptorIndexPrecision(IndexPrecision indexPrecision, PrintContext parameter)
        {
            parameter.WriteLine("Index Precision: {0} bit", indexPrecision.Precision);
        }
        public void AcceptMetafileDescriptorColorPrecision(ColorPrecision colorPrecision, PrintContext parameter)
        {
            parameter.WriteLine("Color Precision: {0} bit", colorPrecision.Precision);
        }
        public void AcceptMetafileDescriptorColorIndexPrecision(ColorIndexPrecision colorIndexPrecision, PrintContext parameter)
        {
            parameter.WriteLine("Color Index Precision: {0} bit", colorIndexPrecision.Precision);
        }
        public void AcceptMetafileDescriptorMaximumColorIndex(MaximumColorIndex maximumColorIndex, PrintContext parameter)
        {
            parameter.WriteLine("Maximum Color Index: {0}", maximumColorIndex.Index);
        }
        public void AcceptMetafileDescriptorColorValueExtent(ColorValueExtent colorValueExtent, PrintContext parameter)
        {
            if (colorValueExtent.ColorSpace == ColorSpace.Unknown)
                parameter.WriteLine("Color Value Extent: Unknown Color Space");
            else if (colorValueExtent.ColorSpace == ColorSpace.CIE)
                parameter.WriteLine("Color Value Extent: CIE {0}+{1}/{2}+{3}/{4}+{5}",
                    colorValueExtent.FirstScale, colorValueExtent.FirstOffset,
                    colorValueExtent.SecondScale, colorValueExtent.SecondOffset,
                    colorValueExtent.ThirdScale, colorValueExtent.ThirdOffset);
            else // RGB or CMYK
                parameter.WriteLine("Color Value Extent: {0} {1}/{2}",
                    colorValueExtent.ColorSpace, colorValueExtent.Minimum, colorValueExtent.Maximum);
        }
        public void AcceptMetafileDescriptorMetafileElementsList(MetafileElementsList metafileElementsList, PrintContext parameter)
        {
            parameter.WriteLine("Metafile Elements List: {0} entries", metafileElementsList.Elements.Count());
            parameter.BeginLevel();
            foreach (string metafileElement in metafileElementsList.Elements)
                parameter.WriteLine(metafileElement);
            parameter.EndLevel();
        }
        public void AcceptMetafileDescriptorMetafileDefaultsReplacement(MetafileDefaultsReplacement metafileDefaultsReplacement, PrintContext parameter)
        {
            parameter.WriteLine("Metafile Defaults Replacement: {0} entries", metafileDefaultsReplacement.Commands.Count());
            parameter.BeginLevel();
            foreach (var command in metafileDefaultsReplacement.Commands)
                command.Accept(this, parameter);
            parameter.EndLevel();
        }
        public void AcceptMetafileDescriptorFontList(FontList fontList, PrintContext parameter)
        {
            parameter.WriteLine("Font List: {0} entries", fontList.Fonts.Count());
            parameter.BeginLevel();
            foreach (string font in fontList.Fonts)
                parameter.WriteLine(font);
            parameter.EndLevel();
        }
        public void AcceptMetafileDescriptorCharacterSetList(CharacterSetList characterSetList, PrintContext parameter)
        {
            parameter.WriteLine("Character Set List: {0} entries", characterSetList.Entries.Count());
            parameter.BeginLevel();
            foreach (var entry in characterSetList.Entries)
                parameter.WriteLine("{0} (Tail {1})",
                    entry.CharacterSetType,
                    string.Join(", ", entry.DesignationSequenceTail.Select(c => ((int)c).ToString("x2"))));
            parameter.EndLevel();
        }
        public void AcceptMetafileDescriptorCharacterCodingAnnouncer(CharacterCodingAnnouncer characterCodingAnnouncer, PrintContext parameter)
        {
            parameter.WriteLine("Character Coding Announcer: {0}", characterCodingAnnouncer.CharacterCodingAnnouncerType);
        }
        public void AcceptMetafileDescriptorNamePrecision(NamePrecision namePrecision, PrintContext parameter)
        {
            parameter.WriteLine("Name Precision: {0} bit", namePrecision.Precision);
        }
        public void AcceptMetafileDescriptorMaximumVdcExtent(MaximumVdcExtent maximumVdcExtent, PrintContext parameter)
        {
            parameter.WriteLine("Maximum VDC Extent: {0} - {1}", maximumVdcExtent.FirstCorner, maximumVdcExtent.SecondCorner);
        }
        public void AcceptMetafileDescriptorSegmentPriorityExtent(SegmentPriorityExtent segmentPriorityExtent, PrintContext parameter)
        {
            parameter.WriteLine("Segment Priority Extent: {0} - {1}", segmentPriorityExtent.MinimumPriorityValue, segmentPriorityExtent.MaximumPriorityValue);
        }
        public void AcceptMetafileDescriptorColorModel(ColorModelCommand colorModel, PrintContext parameter)
        {
            parameter.WriteLine("Color Model: {0}", colorModel.ColorModel);
        }
        public void AcceptMetafileDescriptorFontProperties(FontProperties fontProperties, PrintContext parameter)
        {
            parameter.WriteLine("Font Properties: {0} entries", fontProperties.Properties.Length);
            parameter.BeginLevel();
            foreach (var property in fontProperties.Properties)
                parameter.WriteLine("{0} ({1}), priority {2}", property.Indicator, property.Name, property.Priority);
            parameter.EndLevel();
        }

        public void AcceptPictureDescriptorScalingMode(ScalingMode scalingMode, PrintContext parameter)
        {
            if (scalingMode.ScalingModeType == ScalingModeType.Metric)
                parameter.WriteLine("Scaling Mode: {0} (Factor {1})", scalingMode.ScalingModeType, scalingMode.MetricScalingFactor);
            else
                parameter.WriteLine("Scaling Mode: {0}", scalingMode.ScalingModeType);
        }
        public void AcceptPictureDescriptorColorSelectionMode(ColorSelectionMode colorSelectionMode, PrintContext parameter)
        {
            parameter.WriteLine("Color Selection Mode: {0}", colorSelectionMode.ColorMode);
        }
        public void AcceptPictureDescriptorLineWidthSpecificationMode(LineWidthSpecificationMode lineWidthSpecificationMode, PrintContext parameter)
        {
            parameter.WriteLine("Line Width Specification Mode: {0}", lineWidthSpecificationMode.WidthSpecificationMode);
        }
        public void AcceptPictureDescriptorMarkerSizeSpecificationMode(MarkerSizeSpecificationMode markerSizeSpecificationMode, PrintContext parameter)
        {
            parameter.WriteLine("Marker Size Specification Mode: {0}", markerSizeSpecificationMode.WidthSpecificationMode);
        }
        public void AcceptPictureDescriptorEdgeWidthSpecificationMode(EdgeWidthSpecificationMode edgeWidthSpecificationMode, PrintContext parameter)
        {
            parameter.WriteLine("Edge Width Specification Mode: {0}", edgeWidthSpecificationMode.WidthSpecificationMode);
        }
        public void AcceptPictureDescriptorVdcExtent(VdcExtent vdcExtent, PrintContext parameter)
        {
            parameter.WriteLine("VDC Extent: {0} - {1}", vdcExtent.FirstCorner, vdcExtent.SecondCorner);
        }
        public void AcceptPictureDescriptorBackgroundColor(BackgroundColor backgroundColor, PrintContext parameter)
        {
            parameter.WriteLine("Background Color: {0}", backgroundColor.Color);
        }
        public void AcceptPictureDescriptorDeviceViewport(DeviceViewport deviceViewport, PrintContext parameter)
        {
            parameter.WriteLine("Device Viewport: {0} - {1}", deviceViewport.FirstCorner, deviceViewport.SecondCorner);
        }
        public void AcceptPictureDescriptorDeviceViewportSpecificationMode(DeviceViewportSpecificationMode deviceViewportSpecificationMode, PrintContext parameter)
        {
            if (deviceViewportSpecificationMode.SpecificationMode == DeviceViewportSpecificationModeType.MillimetersWithScaleFactor)
                parameter.WriteLine("Scaling Mode: {0} (Factor {1})", deviceViewportSpecificationMode.SpecificationMode, deviceViewportSpecificationMode.ScaleFactor);
            else
                parameter.WriteLine("Scaling Mode: {0}", deviceViewportSpecificationMode.SpecificationMode);
        }
        public void AcceptPictureDescriptorInteriorStyleSpecificationMode(InteriorStyleSpecificationMode interiorStyleSpecificationMode, PrintContext parameter)
        {
            parameter.WriteLine("Interior Style Specification Mode: {0}", interiorStyleSpecificationMode.WidthSpecificationMode);
        }
        public void AcceptPictureDescriptorLineAndEdgeTypeDefinition(LineAndEdgeTypeDefinition lineAndEdgeTypeDefinition, PrintContext parameter)
        {
            parameter.WriteLine("Line and Edge Type Definition: {0} (dash cycle repeat length: {1}, {2} elements)",
                lineAndEdgeTypeDefinition.LineType, lineAndEdgeTypeDefinition.DashCycleRepeatLength, lineAndEdgeTypeDefinition.DashElements.Length);
        }
        public void AcceptPictureDescriptorHatchStyleDefinition(HatchStyleDefinition hatchStyleDefinition, PrintContext parameter)
        {
            parameter.WriteLine("Hatch Style Definition: {0} (duty cycle length: {1}, {2} gaps, from {3} to {4})",
                hatchStyleDefinition.HatchIndex, hatchStyleDefinition.DutyCycleLength, hatchStyleDefinition.GapWidths.Length,
                hatchStyleDefinition.HatchDirectionStart, hatchStyleDefinition.HatchDirectionEnd);
        }
        public void AcceptPictureDescriptorGeometricPatternDefinition(GeometricPatternDefinition geometricPatternDefinition, PrintContext parameter)
        {
            parameter.WriteLine("Geometric Pattern Definition: {0} (segment {1}, from {2} to {3})",
                geometricPatternDefinition.GeometricPatternIndex, geometricPatternDefinition.SegmentIdentifier,
                geometricPatternDefinition.FirstCorner, geometricPatternDefinition.SecondCorner);
        }

        public void AcceptControlVdcIntegerPrecision(VdcIntegerPrecision vdcIntegerPrecision, PrintContext parameter)
        {
            parameter.WriteLine("VDC Integer Precision: {0} bit", vdcIntegerPrecision.Precision);
        }
        public void AcceptControlVdcRealPrecision(VdcRealPrecision vdcRealPrecision, PrintContext parameter)
        {
            if (vdcRealPrecision.Specification == RealPrecisionSpecification.Unsupported)
                parameter.WriteLine("VDC Real Precision: Unsupported ({0}, {1} bit Exponent width, {2} bit Fraction width)",
                    vdcRealPrecision.RepresentationForm, vdcRealPrecision.ExponentWidth, vdcRealPrecision.FractionWidth);
            else
                parameter.WriteLine("VDC Real Precision: {0}", vdcRealPrecision.Specification);
        }
        public void AcceptControlAuxiliaryColor(AuxiliaryColor auxiliaryColor, PrintContext parameter)
        {
            parameter.WriteLine("Auxiliary Color: {0}", auxiliaryColor.Color);
        }
        public void AcceptControlTransparency(Transparency transparency, PrintContext parameter)
        {
            parameter.WriteLine("Transparency: {0}", transparency.Indicator);
        }
        public void AcceptControlClipRectangle(ClipRectangle clipRectangle, PrintContext parameter)
        {
            parameter.WriteLine("Clip Rectangle: {0} - {1}", clipRectangle.FirstCorner, clipRectangle.SecondCorner);
        }
        public void AcceptControlClipIndicator(ClipIndicator clipIndicator, PrintContext parameter)
        {
            parameter.WriteLine("Clip Indicator: {0}", clipIndicator.Indicator);
        }
        public void AcceptControlLineClippingMode(LineClippingMode lineClippingMode, PrintContext parameter)
        {
            parameter.WriteLine("Line Clipping Mode: {0}", lineClippingMode.Mode);
        }
        public void AcceptControlMarkerClippingMode(MarkerClippingMode markerClippingMode, PrintContext parameter)
        {
            parameter.WriteLine("Marker Clipping Mode: {0}", markerClippingMode.Mode);
        }
        public void AcceptControlEdgeClippingMode(EdgeClippingMode edgeClippingMode, PrintContext parameter)
        {
            parameter.WriteLine("Edge Clipping Mode: {0}", edgeClippingMode.Mode);
        }
        public void AcceptControlNewRegion(NewRegion newRegion, PrintContext parameter)
        {
            parameter.WriteLine("New Region");
        }
        public void AcceptControlSavePrimitiveContext(SavePrimitiveContext savePrimitiveContext, PrintContext parameter)
        {
            parameter.WriteLine("Save Primitive Context: {0}", savePrimitiveContext.ContextName);
        }
        public void AcceptControlRestorePrimitiveContext(RestorePrimitiveContext restorePrimitiveContext, PrintContext parameter)
        {
            parameter.WriteLine("Restore Primitive Context: {0}", restorePrimitiveContext.ContextName);
        }
        public void AcceptControlProtectionRegionIndicator(ProtectionRegionIndicator protectionRegionIndicator, PrintContext parameter)
        {
            parameter.WriteLine("Protection Region Indicator: {0} ({1})", protectionRegionIndicator.Index, protectionRegionIndicator.Indicator);
        }
        public void AcceptControlGeneralizedTextPathMode(GeneralizedTextPathMode generalizedTextPathMode, PrintContext parameter)
        {
            parameter.WriteLine("Generalized Text Path Mode: {0}", generalizedTextPathMode.Mode);
        }
        public void AcceptControlMiterLimit(MiterLimit miterLimit, PrintContext parameter)
        {
            parameter.WriteLine("Mitre Limit: {0}", miterLimit.Limit);
        }

        public void AcceptGraphicalPrimitivePolyline(Polyline polyline, PrintContext parameter)
        {
            parameter.WriteLine("Polyline: {0} points", polyline.Points.Length);
        }
        public void AcceptGraphicalPrimitiveDisjointPolyline(DisjointPolyline disjointPolyline, PrintContext parameter)
        {
            parameter.WriteLine("Disjoint Polyline: {0} points", disjointPolyline.Points.Length);
        }
        public void AcceptGraphicalPrimitivePolymarker(Polymarker polymarker, PrintContext parameter)
        {
            parameter.WriteLine("Polymarker: {0} points", polymarker.Points.Length);
        }
        public void AcceptGraphicalPrimitiveText(TextCommand text, PrintContext parameter)
        {
            parameter.WriteLine("Text: '{0}' (at {1})", text.Text, text.Position);
        }
        public void AcceptGraphicalPrimitiveRestrictedText(RestrictedText restrictedText, PrintContext parameter)
        {
            parameter.WriteLine("Restricted Text: '{0}' (at {1}, +/-{2} by {3})",
                restrictedText.Text, restrictedText.Position, restrictedText.DeltaWidth, restrictedText.DeltaHeight);
        }
        public void AcceptGraphicalPrimitiveAppendText(AppendText appendText, PrintContext parameter)
        {
            parameter.WriteLine("Append Text: '{0}'", appendText.Text);
        }
        public void AcceptGraphicalPrimitivePolygon(Polygon polygon, PrintContext parameter)
        {
            parameter.WriteLine("Polygon: {0} points", polygon.Points.Length);
        }
        public void AcceptGraphicalPrimitivePolygonSet(PolygonSet polygonSet, PrintContext parameter)
        {
            parameter.WriteLine("Polygon Set: {0} points", polygonSet.Points.Length);
        }
        public void AcceptGraphicalPrimitiveCellArray(CellArray cellArray, PrintContext parameter)
        {
            parameter.WriteLine("Cell Array: {0} by {1}", cellArray.NX, cellArray.NY);
        }
        public void AcceptGraphicalPrimitiveRectangle(Rectangle rectangle, PrintContext parameter)
        {
            parameter.WriteLine("Rectangle: {0} - {1}", rectangle.FirstCorner, rectangle.SecondCorner);
        }
        public void AcceptGraphicalPrimitiveCircle(Circle circle, PrintContext parameter)
        {
            parameter.WriteLine("Circle: {0} @ {1}", circle.Center, circle.Radius);
        }
        public void AcceptGraphicalPrimitiveCircularArc3Point(CircularArc3Point circularArc3Point, PrintContext parameter)
        {
            parameter.WriteLine("Circular Arc 3 Point: {0} to {1} to {2}", circularArc3Point.Start, circularArc3Point.Intermediate, circularArc3Point.End);
        }
        public void AcceptGraphicalPrimitiveCircularArc3PointClose(CircularArc3PointClose circularArc3PointClose, PrintContext parameter)
        {
            parameter.WriteLine("Circular Arc 3 Point Close: {0} to {1} to {2} ({3})",
                circularArc3PointClose.Start, circularArc3PointClose.Intermediate, circularArc3PointClose.End, circularArc3PointClose.Closure);
        }
        public void AcceptGraphicalPrimitiveCircularArcCenter(CircularArcCenter circularArcCenter, PrintContext parameter)
        {
            parameter.WriteLine("Circular Arc Center: {0} @ {1} ({2} to {3})", circularArcCenter.Center, circularArcCenter.Radius, circularArcCenter.Start, circularArcCenter.End);
        }
        public void AcceptGraphicalPrimitiveCircularArcCenterClose(CircularArcCenterClose circularArcCenterClose, PrintContext parameter)
        {
            parameter.WriteLine("Circular Arc Center Close: {0} @ {1} ({2} to {3}, {4})",
                circularArcCenterClose.Center, circularArcCenterClose.Radius, circularArcCenterClose.Start, circularArcCenterClose.End, circularArcCenterClose.Closure);
        }
        public void AcceptGraphicalPrimitiveEllipse(Ellipse ellipse, PrintContext parameter)
        {
            parameter.WriteLine("Ellipse: {0} @ {1} - {2}", ellipse.Center, ellipse.FirstConjugateDiameter, ellipse.SecondConjugateDiameter);
        }
        public void AcceptGraphicalPrimitiveEllipticalArc(EllipticalArc ellipticalArc, PrintContext parameter)
        {
            parameter.WriteLine("Elliptical Arc: {0} @ {1} - {2} ({3} to {4})",
                ellipticalArc.Center, ellipticalArc.FirstConjugateDiameter, ellipticalArc.SecondConjugateDiameter, ellipticalArc.Start, ellipticalArc.End);
        }
        public void AcceptGraphicalPrimitiveEllipticalArcClose(EllipticalArcClose ellipticalArcClose, PrintContext parameter)
        {
            parameter.WriteLine("Elliptical Arc Close: {0} @ {1} - {2} ({3} to {4}, {5})",
                ellipticalArcClose.Center,
                ellipticalArcClose.FirstConjugateDiameter, ellipticalArcClose.SecondConjugateDiameter,
                ellipticalArcClose.Start, ellipticalArcClose.End,
                ellipticalArcClose.Closure);
        }
        public void AcceptGraphicalPrimitiveCircularArcCenterReversed(CircularArcCenterReversed circularArcCenterReversed, PrintContext parameter)
        {
            parameter.WriteLine("Circular Arc Center Reversed: {0} @ {1} ({2} to {3})",
                circularArcCenterReversed.Center, circularArcCenterReversed.Radius, circularArcCenterReversed.Start, circularArcCenterReversed.End);
        }
        public void AcceptGraphicalPrimitiveConnectingEdge(ConnectingEdge connectingEdge, PrintContext parameter)
        {
            parameter.WriteLine("Connecting Edge");
        }
        public void AcceptGraphicalPrimitiveHyperbolicArc(HyperbolicArc hyperbolicArc, PrintContext parameter)
        {
            parameter.WriteLine("Hyperbolic Arc: {0} @ {1} - {2} ({3} to {4})",
                hyperbolicArc.Center, hyperbolicArc.TraverseRadiusEndPoint, hyperbolicArc.ConjugateRadiusEndPoint, hyperbolicArc.Start, hyperbolicArc.End);
        }
        public void AcceptGraphicalPrimitiveParabolicArc(ParabolicArc parabolicArc, PrintContext parameter)
        {
            parameter.WriteLine("Parabolic Arc: {0} ({1} to {2})",
                parabolicArc.TangentIntersectionPoint, parabolicArc.Start, parabolicArc.End);
        }
        public void AcceptGraphicalPrimitiveNonUniformBSpline(NonUniformBSpline nonUniformBSpline, PrintContext parameter)
        {
            parameter.WriteLine("Non-Uniform B-Spline: {0} ({1} points, {2} knots, {3} to {4})",
                nonUniformBSpline.SplineOrder, nonUniformBSpline.ControlPoints.Length, nonUniformBSpline.Knots.Length,
                nonUniformBSpline.Start, nonUniformBSpline.End);
        }
        public void AcceptGraphicalPrimitiveNonUniformRationalBSpline(NonUniformRationalBSpline nonUniformRationalBSpline, PrintContext parameter)
        {
            parameter.WriteLine("Non-Uniform Rational B-Spline: {0} ({1} points, {2} knots, {3} to {4})",
                nonUniformRationalBSpline.SplineOrder, nonUniformRationalBSpline.ControlPoints.Length, nonUniformRationalBSpline.Knots.Length,
                nonUniformRationalBSpline.Start, nonUniformRationalBSpline.End);
        }
        public void AcceptGraphicalPrimitivePolybezier(Polybezier polybezier, PrintContext parameter)
        {
            parameter.WriteLine("Polybezier: {0} ({1}, {2} points)", polybezier.ContinuityIndicator, polybezier.Name, polybezier.PointSequences.Length);
        }
        public void AcceptGraphicalPrimitiveBitonalTile(BitonalTile bitonalTile, PrintContext parameter)
        {
            parameter.WriteLine("Bitonal Tile: {0} ({1}, {2} parameters)", bitonalTile.CompressionType, bitonalTile.CompressionTypeName, bitonalTile.Parameters?.Elements.Count());
        }
        public void AcceptGraphicalPrimitiveTile(Tile tile, PrintContext parameter)
        {
            parameter.WriteLine("Tile: {0} ({1}, {2} parameters)", tile.CompressionType, tile.CompressionTypeName, tile.Parameters?.Elements.Count());
        }

        public void AcceptAttributeLineBundleIndex(LineBundleIndex lineBundleIndex, PrintContext parameter)
        {
            parameter.WriteLine("Line Bundle Index: {0}", lineBundleIndex.Index);
        }
        public void AcceptAttributeLineType(LineType lineType, PrintContext parameter)
        {
            parameter.WriteLine("Line Type: {0} ({1})", lineType.Index, lineType.Name);
        }
        public void AcceptAttributeLineWidth(LineWidth lineWidth, PrintContext parameter)
        {
            parameter.WriteLine("Line Width: {0}", lineWidth.Width);
        }
        public void AcceptAttributeLineColor(LineColor lineColor, PrintContext parameter)
        {
            parameter.WriteLine("Line Color: {0}", lineColor.Color);
        }
        public void AcceptAttributeMarkerBundleIndex(MarkerBundleIndex markerBundleIndex, PrintContext parameter)
        {
            parameter.WriteLine("Marker Bundle Index: {0}", markerBundleIndex.Index);
        }
        public void AcceptAttributeMarkerType(MarkerType markerType, PrintContext parameter)
        {
            parameter.WriteLine("Marker Type: {0} ({1})", markerType.Index, markerType.Name);
        }
        public void AcceptAttributeMarkerSize(MarkerSize markerSize, PrintContext parameter)
        {
            parameter.WriteLine("Marker Size: {0}", markerSize.Size);
        }
        public void AcceptAttributeMarkerColor(MarkerColor markerColor, PrintContext parameter)
        {
            parameter.WriteLine("Marker Color: {0}", markerColor.Color);
        }
        public void AcceptAttributeTextBundleIndex(TextBundleIndex textBundleIndex, PrintContext parameter)
        {
            parameter.WriteLine("Text Bundle Index: {0}", textBundleIndex.Index);
        }
        public void AcceptAttributeTextFontIndex(TextFontIndex textFontIndex, PrintContext parameter)
        {
            parameter.WriteLine("Text Font Index: {0}", textFontIndex.Index);
        }
        public void AcceptAttributeTextPrecision(TextPrecision textPrecision, PrintContext parameter)
        {
            parameter.WriteLine("Text Precision: {0}", textPrecision.Precision);
        }
        public void AcceptAttributeCharacterExpansionFactor(CharacterExpansionFactor characterExpansionFactor, PrintContext parameter)
        {
            parameter.WriteLine("Character Expansion Factor: {0}", characterExpansionFactor.Factor);
        }
        public void AcceptAttributeCharacterSpacing(CharacterSpacing characterSpacing, PrintContext parameter)
        {
            parameter.WriteLine("Character Spacing: {0}", characterSpacing.AdditionalIntercharacterSpace);
        }
        public void AcceptAttributeTextColor(TextColor textColor, PrintContext parameter)
        {
            parameter.WriteLine("Text Color: {0}", textColor.Color);
        }
        public void AcceptAttributeCharacterHeight(CharacterHeight characterHeight, PrintContext parameter)
        {
            parameter.WriteLine("Character Height: {0}", characterHeight.Height);
        }
        public void AcceptAttributeCharacterOrientation(CharacterOrientation characterOrientation, PrintContext parameter)
        {
            parameter.WriteLine("Character Orientation: up {0}, base {1}", characterOrientation.Up, characterOrientation.Base);
        }
        public void AcceptAttributeTextPath(TextPath textPath, PrintContext parameter)
        {
            parameter.WriteLine("Text Path: {0}", textPath.Path);
        }
        public void AcceptAttributeTextAlignment(TextAlignment textAlignment, PrintContext parameter)
        {
            parameter.WriteLine("Text Alignment: {0}/{1}", textAlignment.Horizontal, textAlignment.Vertical);
        }
        public void AcceptAttributeCharacterSetIndex(CharacterSetIndex characterSetIndex, PrintContext parameter)
        {
            parameter.WriteLine("Character Set Index: {0}", characterSetIndex.Index);
        }
        public void AcceptAttributeAlternateCharacterSetIndex(AlternateCharacterSetIndex alternateCharacterSetIndex, PrintContext parameter)
        {
            parameter.WriteLine("Alternate Character Set Index: {0}", alternateCharacterSetIndex.Index);
        }
        public void AcceptAttributeFillBundleIndex(FillBundleIndex fillBundleIndex, PrintContext parameter)
        {
            parameter.WriteLine("Fill Bundle Index: {0}", fillBundleIndex.Index);
        }
        public void AcceptAttributeInteriorStyle(InteriorStyle interiorStyle, PrintContext parameter)
        {
            parameter.WriteLine("Interior Style: {0}", interiorStyle.Style);
        }
        public void AcceptAttributeFillColor(FillColor fillColor, PrintContext parameter)
        {
            parameter.WriteLine("Fill Color: {0}", fillColor.Color);
        }
        public void AcceptAttributeHatchIndex(HatchIndex hatchIndex, PrintContext parameter)
        {
            parameter.WriteLine("Hatch Index: {0} ({1})", hatchIndex.Index, hatchIndex.Name);
        }
        public void AcceptAttributePatternIndex(PatternIndex patternIndex, PrintContext parameter)
        {
            parameter.WriteLine("Pattern Index: {0}", patternIndex.Index);
        }
        public void AcceptAttributeEdgeBundleIndex(EdgeBundleIndex edgeBundleIndex, PrintContext parameter)
        {
            parameter.WriteLine("Edge Bundle Index: {0}", edgeBundleIndex.Index);
        }
        public void AcceptAttributeEdgeType(EdgeType edgeType, PrintContext parameter)
        {
            parameter.WriteLine("Edge Type Index: {0} ({1})", edgeType.Index, edgeType.Name);
        }
        public void AcceptAttributeEdgeWidth(EdgeWidth edgeWidth, PrintContext parameter)
        {
            parameter.WriteLine("Edge Width: {0}", edgeWidth.Width);
        }
        public void AcceptAttributeEdgeColor(EdgeColor edgeColor, PrintContext parameter)
        {
            parameter.WriteLine("Edge Color: {0}", edgeColor.Color);
        }
        public void AcceptAttributeEdgeVisibility(EdgeVisibility edgeVisibility, PrintContext parameter)
        {
            parameter.WriteLine("Edge Visibility: {0}", edgeVisibility.Visibility);
        }
        public void AcceptAttributeFillReferencePoint(FillReferencePoint fillReferencePoint, PrintContext parameter)
        {
            parameter.WriteLine("Fill Reference Point: {0}", fillReferencePoint.ReferencePoint);
        }
        public void AcceptAttributePatternTable(PatternTable patternTable, PrintContext parameter)
        {
            parameter.WriteLine("Pattern Table: {0} ({1} by {2})", patternTable.Index, patternTable.Width, patternTable.Height);
        }
        public void AcceptAttributePatternSize(PatternSize patternSize, PrintContext parameter)
        {
            parameter.WriteLine("Pattern Size: {0} by {1}", patternSize.Width, patternSize.Height);
        }
        public void AcceptAttributeColorTable(ColorTable colorTable, PrintContext parameter)
        {
            parameter.WriteLine("Color Table: update from {0} with {1} colors", colorTable.StartIndex, colorTable.Colors.Length);
        }
        public void AcceptAttributeAspectSourceFlags(AspectSourceFlags aspectSourceFlags, PrintContext parameter)
        {
            parameter.WriteLine("Aspect Source Flags: {0} entries", aspectSourceFlags.Values.Count);
            parameter.BeginLevel();
            foreach (var kvp in aspectSourceFlags.Values)
                parameter.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            parameter.EndLevel();
        }
        public void AcceptAttributePickIdentifier(PickIdentifier pickIdentifier, PrintContext parameter)
        {
            parameter.WriteLine("Pick Identifier: {0}", pickIdentifier.Identifier);
        }
        public void AcceptAttributeLineCap(LineCap lineCap, PrintContext parameter)
        {
            parameter.WriteLine("Line Cap: line {0} ({1}), dash {2} ({3})",
                lineCap.LineCapIndicator, lineCap.LineCapName,
                lineCap.DashCapIndicator, lineCap.DashCapName);
        }
        public void AcceptAttributeLineJoin(LineJoin lineJoin, PrintContext parameter)
        {
            parameter.WriteLine("Line Join: {0} ({1})", lineJoin.Index, lineJoin.Name);
        }
        public void AcceptAttributeLineTypeContinuation(LineTypeContinuation lineTypeContinuation, PrintContext parameter)
        {
            parameter.WriteLine("Line Type Continuation: {0} ({1})", lineTypeContinuation.Index, lineTypeContinuation.Name);
        }
        public void AcceptAttributeLineTypeInitialOffset(LineTypeInitialOffset lineTypeInitialOffset, PrintContext parameter)
        {
            parameter.WriteLine("Line Type Initial Offset: {0}", lineTypeInitialOffset.Offset);
        }
        public void AcceptAttributeRestrictedTextType(RestrictedTextType restrictedTextType, PrintContext parameter)
        {
            parameter.WriteLine("Restricted Text Type: {0} ({1})", restrictedTextType.Index, restrictedTextType.Name);
        }
        public void AcceptAttributeInterpolatedInterior(InterpolatedInterior interpolatedInterior, PrintContext parameter)
        {
            parameter.WriteLine("Interpolated Interior: {0} ({1})", interpolatedInterior.Index, interpolatedInterior.Name);
            parameter.BeginLevel();
            parameter.WriteLine("Reference Geometry: {0}", string.Join(" ", interpolatedInterior.ReferenceGeometry));
            parameter.WriteLine("Stage Designators: {0}", string.Join(" ", interpolatedInterior.StageDesignators));
            parameter.WriteLine("Color Specifiers: {0}", string.Join(" ", interpolatedInterior.ColorSpecifiers.Select(c => c.ToString())));
            parameter.EndLevel();
        }
        public void AcceptAttributeEdgeCap(EdgeCap edgeCap, PrintContext parameter)
        {
            parameter.WriteLine("Edge Cap: line {0} ({1}), dash {2} ({3})",
                edgeCap.EdgeCapIndicator, edgeCap.EdgeCapName,
                edgeCap.DashCapIndicator, edgeCap.DashCapName);
        }
        public void AcceptAttributeEdgeJoin(EdgeJoin edgeJoin, PrintContext parameter)
        {
            parameter.WriteLine("Edge Join: {0} ({1})", edgeJoin.Index, edgeJoin.Name);
        }
        public void AcceptAttributeEdgeTypeContinuation(EdgeTypeContinuation edgeTypeContinuation, PrintContext parameter)
        {
            parameter.WriteLine("Edge Type Continuation: {0} ({1})", edgeTypeContinuation.Index, edgeTypeContinuation.Name);
        }
        public void AcceptAttributeEdgeTypeInitialOffset(EdgeTypeInitialOffset edgeTypeInitialOffset, PrintContext parameter)
        {
            parameter.WriteLine("Edge Type Initial Offset: {0}", edgeTypeInitialOffset.Offset);
        }

        public void AcceptEscapeEscape(EscapeCommand escapeCommand, PrintContext parameter)
        {
            parameter.WriteLine("Escape: {0} ({1}) '{2}'", escapeCommand.Identifier, escapeCommand.Name, escapeCommand.DataRecord);
        }

        public void AcceptExternalMessage(Message message, PrintContext parameter)
        {
            parameter.WriteLine("Message: {0} '{1}'", message.ActionRequired, message.MessageString);
        }
        public void AcceptExternalApplicationData(ApplicationData applicationData, PrintContext parameter)
        {
            parameter.WriteLine("Application Data: {0} '{1}'", applicationData.Identifier, applicationData.DataRecord);
        }

        public void AcceptSegmentCopySegment(CopySegment copySegment, PrintContext parameter)
        {
            parameter.WriteLine("Copy Segment: {0} -> {1} (applied: {2})", copySegment.SegmentIdentifier, copySegment.Matrix, copySegment.TransformationApplication);
        }
        public void AcceptSegmentInheritanceFilter(InheritanceFilter inheritanceFilter, PrintContext parameter)
        {
            parameter.WriteLine("Inheritance Filter: {0} entries", inheritanceFilter.Items.Length);
            parameter.BeginLevel();
            foreach (var item in inheritanceFilter.Items)
                parameter.WriteLine("{0}: {1}", item.Designator, item.Setting);
            parameter.EndLevel();
        }
        public void AcceptSegmentClipInheritance(ClipInheritance clipInheritance, PrintContext parameter)
        {
            parameter.WriteLine("Clip Inheritance: {0}", clipInheritance.InheritanceType);
        }
        public void AcceptSegmentSegmentTransformation(SegmentTransformation segmentTransformation, PrintContext parameter)
        {
            parameter.WriteLine("Segment Transformation: {0} -> {1}", segmentTransformation.SegmentIdentifier, segmentTransformation.Matrix);
        }
        public void AcceptSegmentSegmentHighlighting(SegmentHighlighting segmentHighlighting, PrintContext parameter)
        {
            parameter.WriteLine("Segment Highlighting: {0} -> {1}", segmentHighlighting.SegmentIdentifier, segmentHighlighting.Highlighting);
        }
        public void AcceptSegmentSegmentDisplayPriority(SegmentDisplayPriority segmentDisplayPriority, PrintContext parameter)
        {
            parameter.WriteLine("Segment Display Priority: {0} -> {1}", segmentDisplayPriority.SegmentIdentifier, segmentDisplayPriority.Priority);
        }
        public void AcceptSegmentSegmentPickPriority(SegmentPickPriority segmentPickPriority, PrintContext parameter)
        {
            parameter.WriteLine("Segment Pick Priority: {0} -> {1}", segmentPickPriority.SegmentIdentifier, segmentPickPriority.Priority);
        }

        public void AcceptApplicationStructureDescriptorAttribute(ApplicationStructureAttribute applicationStructureAttribute, PrintContext parameter)
        {
            parameter.WriteLine("Attribute: {0} '{1}'", applicationStructureAttribute.AttributeType, applicationStructureAttribute.DataRecord);
        }

        public void AcceptUnsupportedCommand(UnsupportedCommand unsupportedCommand, PrintContext parameter)
        {
            // do nothing; otherwise we'd probably spam the command line
        }
    }
}
