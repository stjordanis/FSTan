﻿module FSTan.Data.List

open FSTan.HKT
open FSTan.Monad

type hlist<'a> = hkt<HList, 'a>
and HList() =
    inherit monad<HList>() with
        override __.bind<'a, 'b> (m: hlist<'a>) (k: 'a -> hlist<'b>) =
            let f x = 
                let m: hlist<'b> = k x
                let lst : 'b list = unwrap m
                lst
            wrap <| List.collect f (unwrap m)

        override __.pure'<'a> (a: 'a) : hlist<'a> = wrap <| [a]
 
        static member inline wrap<'a> (x : List<'a>): hlist<'a> =  {wrap = x} :> _
        static member inline unwrap<'a> (x : hlist<'a>): List<'a> =  (x :?> _).wrap

and hListData<'a> = 
    {wrap : List<'a>}
    interface hlist<'a>

