type Student = StudentId of string
type Ticks = T1 | T2 | T3 | T4
type Tests = Test1 | Test2
type Assessment = Tick of tck: Ticks * maxMark: int | Test of tst: Tests * maxMark: int
type MarkKind = Resit | Exception | Normal
type Mark = int * MarkKind
type Marking = Student * Mark * Assessment // one mark
type MCData = Set<Student*Assessment> // specifies students with MC
type Markings = List<Marking>
type TotalMark = Student * int

let totalMarks (name:Student) (mcdata:MCData) (aa:Markings)=
    let checkValidScore (aa:Markings) =
        match aa with
        |((StudentId(x), (y,MK), (Tick(z,mm))) , a) -> match mm < y with 
                                                        |true -> failwithf "not possible value of score %A" aa
                                                        |false -> ((StudentId(x), (y,MK), (Tick(z,mm))) , a)
        |((StudentId(x), (y,_), (Test(b,mm))) , a) -> match mm < y with 
                                                        |true -> failwithf "not possible value of score %A" aa
                                                        |false -> ((StudentId(x), (y,Normal), (Test(b,mm))) , a )

    let matchmcdata (mcdata:MCData) (aa:Markings) =
        match aa with
        |((StudentId(x), (y,MK), (Tick(z,mm))) , _ ) -> ((StudentId(x), (y,MK), (Tick(z,mm)))  , (mcdata.Contains (StudentId(x), (Tick(z,mm)))))
        |((StudentId(x), (y,_), (Test(b,mm))) , _ ) -> ((StudentId(x), (y,Normal), (Test(b,mm))) , false ) 
    
    
    let useMCdata (aa:MCData)=
        match aa with
        |((StudentId(x), (y,_), (Tick(z,mm))) , true ) -> (StudentId(x) , y , "z")
        |((StudentId(x), (y,Resit), (Tick(z,mm))) , false) -> (StudentId(x) , y/2 , "z")
        |((StudentId(x), (y,_), (Tick(z,mm))) , false ) -> (StudentId(x) , y , "z")
        |((StudentId(x), (y,_), (Test(b,mm))) , _ ) -> (StudentId(x) , y , "b")

    

 



