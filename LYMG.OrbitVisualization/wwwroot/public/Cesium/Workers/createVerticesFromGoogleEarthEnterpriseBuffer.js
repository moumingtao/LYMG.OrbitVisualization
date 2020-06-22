define(["./when-54c2dc71","./Check-6c0211bc","./Math-fc8cecf5","./Cartesian2-a8ce88a9","./Transforms-9ac5213c","./RuntimeError-2109023a","./WebGLConstants-76bb35d1","./ComponentDatatype-6d99a1ee","./AttributeCompression-88d6db09","./IntersectionTests-51503d7b","./Plane-29afec1f","./WebMercatorProjection-163698a4","./createTaskProcessorWorker","./EllipsoidTangentPlane-97448adb","./OrientedBoundingBox-c05868e1","./TerrainEncoding-1b092c0a"],function(Wt,t,Ft,Ot,Yt,kt,e,i,a,n,r,Ut,o,Vt,Ht,Lt){"use strict";var Dt=Uint16Array.BYTES_PER_ELEMENT,Gt=Int32Array.BYTES_PER_ELEMENT,jt=Uint32Array.BYTES_PER_ELEMENT,zt=Float32Array.BYTES_PER_ELEMENT,qt=Float64Array.BYTES_PER_ELEMENT;function Jt(t,e,i){i=Wt.defaultValue(i,Ft.CesiumMath);for(var a=t.length,n=0;n<a;++n)if(i.equalsEpsilon(t[n],e,Ft.CesiumMath.EPSILON12))return n;return-1}var Kt=new Ot.Cartographic,Qt=new Ot.Cartesian3,Xt=new Ot.Cartesian3,Zt=new Ot.Cartesian3,$t=new Yt.Matrix4;function te(t,e,i,a,n,r,o,s,u,h){for(var c=o.length,d=0;d<c;++d){var g=o[d],l=g.cartographic,m=g.index,p=t.length,I=l.longitude,v=l.latitude,v=Ft.CesiumMath.clamp(v,-Ft.CesiumMath.PI_OVER_TWO,Ft.CesiumMath.PI_OVER_TWO),E=l.height-r.skirtHeight;r.hMin=Math.min(r.hMin,E),Ot.Cartographic.fromRadians(I,v,E,Kt),u&&(Kt.longitude+=s),u?d===c-1?Kt.latitude+=h:0===d&&(Kt.latitude-=h):Kt.latitude+=s;var T=r.ellipsoid.cartographicToCartesian(Kt);t.push(T),e.push(E),i.push(Ot.Cartesian2.clone(i[m])),0<a.length&&a.push(a[m]),Yt.Matrix4.multiplyByPoint(r.toENU,T,Qt);var C=r.minimum,f=r.maximum;Ot.Cartesian3.minimumByComponent(Qt,C,C),Ot.Cartesian3.maximumByComponent(Qt,f,f);var M,N=r.lastBorderPoint;Wt.defined(N)&&(M=N.index,n.push(M,p-1,p,p,m,M)),r.lastBorderPoint=g}}return o(function(t,e){t.ellipsoid=Ot.Ellipsoid.clone(t.ellipsoid),t.rectangle=Ot.Rectangle.clone(t.rectangle);var i=function(t,e,i,a,n,r,o,s,u,h){var c,d,g,l,m,p;p=Wt.defined(a)?(c=a.west,d=a.south,g=a.east,l=a.north,m=a.width,a.height):(c=Ft.CesiumMath.toRadians(n.west),d=Ft.CesiumMath.toRadians(n.south),g=Ft.CesiumMath.toRadians(n.east),l=Ft.CesiumMath.toRadians(n.north),m=Ft.CesiumMath.toRadians(a.width),Ft.CesiumMath.toRadians(a.height));var I,v,E=[d,l],T=[c,g],C=Yt.Transforms.eastNorthUpToFixedFrame(e,i),f=Yt.Matrix4.inverseTransformation(C,$t);s&&(I=Ut.WebMercatorProjection.geodeticLatitudeToMercatorAngle(d),v=1/(Ut.WebMercatorProjection.geodeticLatitudeToMercatorAngle(l)-I));var M=new DataView(t),N=Number.POSITIVE_INFINITY,x=Number.NEGATIVE_INFINITY,b=Xt;b.x=Number.POSITIVE_INFINITY,b.y=Number.POSITIVE_INFINITY,b.z=Number.POSITIVE_INFINITY;var S=Zt;S.x=Number.NEGATIVE_INFINITY,S.y=Number.NEGATIVE_INFINITY,S.z=Number.NEGATIVE_INFINITY;var w,P,B=0,y=0,A=0;for(P=0;P<4;++P){var R=B;w=M.getUint32(R,!0),R+=jt;var _=Ft.CesiumMath.toRadians(180*M.getFloat64(R,!0));R+=qt,-1===Jt(T,_)&&T.push(_);var W=Ft.CesiumMath.toRadians(180*M.getFloat64(R,!0));R+=qt,-1===Jt(E,W)&&E.push(W),R+=2*qt;var F=M.getInt32(R,!0);R+=Gt,y+=F,F=M.getInt32(R,!0),A+=3*F,B+=w+jt}var O=[],Y=[],k=new Array(y),U=new Array(y),V=new Array(y),H=s?new Array(y):[],L=new Array(A),D=[],G=[],j=[],z=[],q=0,J=0;for(P=B=0;P<4;++P){w=M.getUint32(B,!0);var K=B+=jt,Q=Ft.CesiumMath.toRadians(180*M.getFloat64(B,!0));B+=qt;var X=Ft.CesiumMath.toRadians(180*M.getFloat64(B,!0));B+=qt;var Z=Ft.CesiumMath.toRadians(180*M.getFloat64(B,!0)),$=.5*Z;B+=qt;var tt=Ft.CesiumMath.toRadians(180*M.getFloat64(B,!0)),et=.5*tt;B+=qt;var it=M.getInt32(B,!0);B+=Gt;var at=M.getInt32(B,!0);B+=Gt,B+=Gt;for(var nt=new Array(it),rt=0;rt<it;++rt){var ot=Q+M.getUint8(B++)*Z;Kt.longitude=ot;var st=X+M.getUint8(B++)*tt;Kt.latitude=st;var ut=M.getFloat32(B,!0);if(B+=zt,0!==ut&&ut<h&&(ut*=-Math.pow(2,u)),ut*=6371010*r,Kt.height=ut,-1!==Jt(T,ot)||-1!==Jt(E,st)){var ht=Jt(O,Kt,Ot.Cartographic);if(-1!==ht){nt[rt]=Y[ht];continue}O.push(Ot.Cartographic.clone(Kt)),Y.push(q)}nt[rt]=q,Math.abs(ot-c)<$?D.push({index:q,cartographic:Ot.Cartographic.clone(Kt)}):Math.abs(ot-g)<$?j.push({index:q,cartographic:Ot.Cartographic.clone(Kt)}):Math.abs(st-d)<et?G.push({index:q,cartographic:Ot.Cartographic.clone(Kt)}):Math.abs(st-l)<et&&z.push({index:q,cartographic:Ot.Cartographic.clone(Kt)}),N=Math.min(ut,N),x=Math.max(ut,x),V[q]=ut;var ct=i.cartographicToCartesian(Kt);k[q]=ct,s&&(H[q]=(Ut.WebMercatorProjection.geodeticLatitudeToMercatorAngle(st)-I)*v),Yt.Matrix4.multiplyByPoint(f,ct,Qt),Ot.Cartesian3.minimumByComponent(Qt,b,b),Ot.Cartesian3.maximumByComponent(Qt,S,S);var dt=(ot-c)/(g-c);dt=Ft.CesiumMath.clamp(dt,0,1);var gt=(st-d)/(l-d);gt=Ft.CesiumMath.clamp(gt,0,1),U[q]=new Ot.Cartesian2(dt,gt),++q}for(var lt=3*at,mt=0;mt<lt;++mt,++J)L[J]=nt[M.getUint16(B,!0)],B+=Dt;if(w!==B-K)throw new kt.RuntimeError("Invalid terrain tile.")}k.length=q,U.length=q,V.length=q,s&&(H.length=q);var pt=q,It=J,vt={hMin:N,lastBorderPoint:void 0,skirtHeight:o,toENU:f,ellipsoid:i,minimum:b,maximum:S};D.sort(function(t,e){return e.cartographic.latitude-t.cartographic.latitude}),G.sort(function(t,e){return t.cartographic.longitude-e.cartographic.longitude}),j.sort(function(t,e){return t.cartographic.latitude-e.cartographic.latitude}),z.sort(function(t,e){return e.cartographic.longitude-t.cartographic.longitude});var Et=1e-5;{var Tt,Ct,ft;te(k,V,U,H,L,vt,D,-Et*m,!0,-Et*p),te(k,V,U,H,L,vt,G,-Et*p,!1),te(k,V,U,H,L,vt,j,Et*m,!0,Et*p),te(k,V,U,H,L,vt,z,Et*p,!1),0<D.length&&0<z.length&&(Tt=D[0].index,Ct=z[z.length-1].index,ft=k.length-1,L.push(Ct,ft,pt,pt,Tt,Ct))}y=k.length;var Mt,Nt=Yt.BoundingSphere.fromPoints(k);Wt.defined(a)&&(Mt=Ht.OrientedBoundingBox.fromRectangle(a,N,x,i));for(var xt=new Lt.EllipsoidalOccluder(i).computeHorizonCullingPointPossiblyUnderEllipsoid(e,k,N),bt=new Vt.AxisAlignedBoundingBox(b,S,e),St=new Lt.TerrainEncoding(bt,vt.hMin,x,C,!1,s),wt=new Float32Array(y*St.getStride()),Pt=0,Bt=0;Bt<y;++Bt)Pt=St.encode(wt,Pt,k[Bt],U[Bt],V[Bt],void 0,H[Bt]);var yt=D.map(function(t){return t.index}).reverse(),At=G.map(function(t){return t.index}).reverse(),Rt=j.map(function(t){return t.index}).reverse(),_t=z.map(function(t){return t.index}).reverse();return At.unshift(Rt[Rt.length-1]),At.push(yt[0]),_t.unshift(yt[yt.length-1]),_t.push(Rt[0]),{vertices:wt,indices:new Uint16Array(L),maximumHeight:x,minimumHeight:N,encoding:St,boundingSphere3D:Nt,orientedBoundingBox:Mt,occludeePointInScaledSpace:xt,vertexCountWithoutSkirts:pt,indexCountWithoutSkirts:It,westIndicesSouthToNorth:yt,southIndicesEastToWest:At,eastIndicesNorthToSouth:Rt,northIndicesWestToEast:_t}}(t.buffer,t.relativeToCenter,t.ellipsoid,t.rectangle,t.nativeRectangle,t.exaggeration,t.skirtHeight,t.includeWebMercatorT,t.negativeAltitudeExponentBias,t.negativeElevationThreshold),a=i.vertices;e.push(a.buffer);var n=i.indices;return e.push(n.buffer),{vertices:a.buffer,indices:n.buffer,numberOfAttributes:i.encoding.getStride(),minimumHeight:i.minimumHeight,maximumHeight:i.maximumHeight,boundingSphere3D:i.boundingSphere3D,orientedBoundingBox:i.orientedBoundingBox,occludeePointInScaledSpace:i.occludeePointInScaledSpace,encoding:i.encoding,vertexCountWithoutSkirts:i.vertexCountWithoutSkirts,indexCountWithoutSkirts:i.indexCountWithoutSkirts,westIndicesSouthToNorth:i.westIndicesSouthToNorth,southIndicesEastToWest:i.southIndicesEastToWest,eastIndicesNorthToSouth:i.eastIndicesNorthToSouth,northIndicesWestToEast:i.northIndicesWestToEast}})});
