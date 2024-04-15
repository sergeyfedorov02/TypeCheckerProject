// ERROR_AMBIGUOUS_THROW_TYPE
language core;
extend with #exceptions, #exception-type-declaration;

fn fail(n : Nat) -> Bool {
    return throw(succ(0))
}

fn main(n : Nat) -> Bool {
    return try { fail(n) } catch { a => true }
}
